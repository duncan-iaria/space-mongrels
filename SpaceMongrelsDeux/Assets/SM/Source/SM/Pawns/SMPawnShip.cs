using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNDL;

namespace SM
{
    public class SMPawnShip : SMPawn, IDamageable, ITargetable
    {
        public SMShip ship;
        public SMReactor reactor;
        public SMSensorController sensorController;
        // These are set by the ship object
        [HideInInspector]
        public float moveSpeed, horizontalDampening = .5f, rotationSpeed, boostSpeed, boostCooldown, thrustSpeed;
        public float thrustThreshold = 0.1f;

        [HideInInspector]
        public int maxHealth, currentHealth;

        protected float nextBoostTime;
        protected bool isThrustEligible = true;

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
                case InputButton.CycleRight:
                    sensorController.selectNextTarget();
                    break;
                case InputButton.CycleLeft:
                    sensorController.selectPreviousTarget();
                    break;
                default:
                    break;
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
                reactor.boost(_rigidbody, thrustSpeed);
                isThrustEligible = false;
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
            SMGame tempGame = SMGame.GetGame<SMGame>();
            tempGame.onLoadLevel(1, .5f, true);
        }

        //=======================
        // Targeting
        //=======================
        public void setSelected(GameObject tReticule)
        {
            if (tReticule != null)
            {
                tReticule.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
                tReticule.transform.SetParent(this.transform);
            }
        }
    }
}