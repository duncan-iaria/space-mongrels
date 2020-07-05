using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace SM
{
  public class SMMoveToward : Action
  {
    public SharedFloat moveSpeed;
    public SharedFloat rotationSpeed;
    public SharedTransform currentTarget;

    [BehaviorDesigner.Runtime.Tasks.Tooltip("How close the pawn needs to be to consider this task a success")]
    public float successDistance = 0.25f;
    private SMPawnShip _shipPawn;
    private float collisionCheckSweepAngle = 45;


    public override void OnStart()
    {
      base.OnStart();
      _shipPawn = this.transform.GetComponent<SMPawnShip>();
      if (_shipPawn == null)
      {
        Debug.LogWarning("NO PAWN FOUND FOR AI");
      }
    }

    public override TaskStatus OnUpdate()
    {
      if (currentTarget.Value == null)
      {
        return TaskStatus.Failure;
      }

      // Return a task status of success once we've reached the target
      if (Vector3.SqrMagnitude(transform.position - currentTarget.Value.position) < successDistance)
      {
        return TaskStatus.Success;
      }


      checkCollisions();

      _shipPawn.rotateTowardTarget(currentTarget.Value);
      _shipPawn.moveForward();
      return TaskStatus.Running;
    }

    void checkCollisions()
    {
      // RaycastHit2D hit;
      // hit = Physics2D.CircleCast(transform.position, 1f, transform.up);

      // if (hit.collider != null)
      // {
      //   Debug.Log("hit " + hit.collider.name);
      // }


      RaycastHit2D hitR = Physics2D.Raycast(transform.position, (transform.up + (transform.right * collisionCheckSweepAngle)).normalized, 5f);
      RaycastHit2D hitL = Physics2D.Raycast(transform.position, (transform.up - (transform.right * collisionCheckSweepAngle)).normalized, 5f);
      Debug.DrawRay(transform.position, (transform.up + (transform.right * collisionCheckSweepAngle)).normalized * 5f, Color.red);
      Debug.DrawRay(transform.position, (transform.up - (transform.right * collisionCheckSweepAngle)).normalized * 5f, Color.blue);


      //a general hit check, and checks to see if it's the player
      if (hitR.collider != null || hitL.collider != null)
      {
        //check what kind of hit we've got
        //hit both
        if (hitR.collider != null && hitL.collider != null)
        {
          Debug.Log("Hit BOTH!!!- REVERSE");
          // reverse();
          //Debug.Log( hitR.collider + " " + hitL.collider );
          //Debug.Log( "Avoid MIDDLE!" );
        }
        //hit right
        else if (hitR.collider != null)
        {
          if (hitR.collider.CompareTag("Player"))
          {
            // pawn.controller.engageTarget = hitR.collider.transform;
            // pawn.controller.currentState.toEngageState();
            return;
          }
          // checkLeft();
          Debug.Log("Hit Left");
        }
        //hit left
        else if (hitL.collider != null)
        {
          if (hitL.collider.CompareTag("Player"))
          {
            // pawn.controller.engageTarget = hitL.collider.transform;
            // pawn.controller.currentState.toEngageState();
            return;
          }
          // checkRight();
          Debug.Log("Hit Right");
        }

        //turn on avoidance mode
        // pawn.engageAvoidanceMode();
      }
      //no hit
      else
      {
        // Hit nothing

        //start moving again
        // pawn.resetSpeed();
      }
    }
  }
}
