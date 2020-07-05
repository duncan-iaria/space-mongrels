﻿using SNDL;
using UnityEngine;

namespace SM
{
  public class SMPawnShip : SMPawn, IDamageable, ITargetable
  {
    [Header("Ship Data")]
    public SMShip ship;
    public SMReactor reactor;
    public float thrustThreshold = 0.1f;
    public float allowedRotationalDeviation = 0.1f;

    [Header("Controllers")]
    public SMSensorController sensorController;
    public SMTurretController turretController;

    [Header("Debug")]
    public bool isDebugMode = true;

    // These are set by the ship object SOs (Reactor/Sensors, etc)
    [HideInInspector]
    public float moveSpeed, horizontalDampening = .5f, rotationSpeed, boostSpeed, boostCooldown, thrustSpeed;

    [HideInInspector]
    public int maxHealth, currentHealth;


    protected float nextBoostTime;
    protected bool isThrustEligible = true;

    // This is for providing some dampening to the rotation of the AI
    // When they're rotating the ship 
    protected float AIRotationDampening = 0.75f;

    //=======================
    // Initialization
    //=======================
    protected override void Awake()
    {
      base.Awake();
      rebuildShipData();
    }

    protected virtual void rebuildShipData()
    {
      if (ship != null && reactor != null)
      {
        ship.initialize(gameObject);
        reactor.initialize(gameObject);
        sensorController.clearAllTargets();
      }
    }

    //=======================
    // Pawn Ship Controls
    //=======================
    public override void onInputButton(InputButton tButton)
    {
      switch (tButton)
      {
        case InputButton.Menu:
          loadShipInterior();
          break;
        case InputButton.Boost:
          boost();
          break;
        case InputButton.Accept:
          onAccept();
          break;
        case InputButton.CycleRight:
          sensorController.selectNextTarget();
          break;
        case InputButton.CycleLeft:
          sensorController.selectPreviousTarget();
          break;
        case InputButton.SpecialAction1:
          turretController.toggleWeaponsFree();
          break;
        default:
          break;
      }
    }

    //=======================
    // Pawn AI Controls
    //=======================
    public void moveForward(float tValue = 1)
    {
      onAxis(InputAxis.Vertical, tValue);
    }

    public void rotateTowardTarget(Transform tTarget)
    {
      if (tTarget != null)
      {
        // we use the right vector to tell what direction it needs to correct in
        float dotProd = Vector2.Dot((Vector2)transform.right, (Vector2)(tTarget.position - transform.position).normalized);
        float directionalDotProd = Vector2.Dot((Vector2)transform.up, (Vector2)(tTarget.position - transform.position).normalized);

        // if the direction moving and the direction facing is close to opposite (-1 being opposite) then apply additional boost
        // multiplied by dotProd so that the closer it is to target rotation, the slower it rotates 
        if (dotProd > allowedRotationalDeviation)
        {
          // Rotate Right
          rotate(-1 * AIRotationDampening);
        }
        else if (dotProd < -allowedRotationalDeviation || directionalDotProd < -.7f)
        {
          // Rotate Left
          rotate(1 * AIRotationDampening);
        }
      }
    }

    public override void onAxis(InputAxis tAxis, float tValue)
    {
      switch (tAxis)
      {
        case InputAxis.Vertical:
          handleVertical(tValue);
          break;
        case InputAxis.Horizontal:
          accelerate(new Vector2(tValue, 0f), horizontalDampening);
          break;
        case InputAxis.RightHorizontal:
          rotate(tValue);
          break;
        case InputAxis.RightVertical:
          break;
        default:
          break;
      }
    }

    //=======================
    // Movement
    //=======================
    // When pressing forward/backward
    protected virtual void handleVertical(float tValue)
    {
      if (tValue > 0)
      {
        accelerate(new Vector2(0f, tValue));
        thrust(tValue);
      }
      else
      {
        accelerate(new Vector2(0f, tValue * horizontalDampening));
        isThrustEligible = true;
      }
    }

    protected virtual void accelerate(Vector2 tVector, float tDampening = 1f)
    {
      _rigidbody.AddForce((transform.right * tVector.x * tDampening) * moveSpeed * Time.deltaTime);
      _rigidbody.AddForce((transform.up * tVector.y) * moveSpeed * Time.deltaTime);
    }

    protected virtual void rotate(float tRotationValue)
    {
      _rigidbody.AddTorque(tRotationValue * rotationSpeed * Time.deltaTime);
    }

    // boost is an action taken by the player via a special input button
    protected virtual void boost()
    {
      if (Time.time >= nextBoostTime)
      {
        reactor.boost(_rigidbody, boostSpeed);
        nextBoostTime = Time.time + boostCooldown;
      }
    }

    // Thrust is considered when an engine/reactor first accelerates
    protected virtual void thrust(float tYAxis)
    {
      // mini boost
      if (tYAxis >= thrustThreshold && isThrustEligible)
      {
        Debug.Log("Mini boosted");
        reactor.boost(_rigidbody, thrustSpeed);
        isThrustEligible = false;
      }
    }

    //=======================
    // Controls
    //=======================
    protected virtual void onAccept()
    {
      if (currentInteractable != null)
      {
        currentInteractable.onInteract();
      }
    }

    //=======================
    // Collision
    //=======================
    protected virtual void OnCollisionEnter2D(Collision2D tCollision)
    {
      if (_rigidbody)
      {
        ship.onCollision(this.gameObject, tCollision);
      }
    }

    //=======================
    // Health/Damage
    //=======================
    public void applyDamage(int tDamage)
    {
      ship.takeDamage(this.gameObject, tDamage);
    }

    //=======================
    // Return to Interior
    //=======================
    protected virtual void loadShipInterior()
    {
      if (ship.interiorLevel != null)
      {
        SMGame tempGame = SMGame.GetGame<SMGame>();
        tempGame.loadLevel(ship.interiorLevel);
      }
      else
      {
        Debug.LogWarning("No Interior found to load.");
      }
    }

    //=======================
    // Targeting
    //=======================
    // TODO current unused, but we may want to make use?
    public void setSelected(GameObject tReticule)
    {
      if (tReticule != null)
      {
        tReticule.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        tReticule.transform.SetParent(this.transform);
      }
    }

    public void OnDrawGizmos()
    {
      if (false)
      {
        float collisionCheckSweepAngle = 15f;
        float visualSensorRange = this.sensorController.sensor.range * 0.5f;


        Vector3 leftAngleDirection = Quaternion.AngleAxis(collisionCheckSweepAngle, Vector3.forward) * transform.up;
        Vector3 rightAngleDirection = Quaternion.AngleAxis(-collisionCheckSweepAngle, Vector3.forward) * transform.up;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, leftAngleDirection * visualSensorRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, rightAngleDirection * visualSensorRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, transform.up * visualSensorRange);
      }
    }
  }
}