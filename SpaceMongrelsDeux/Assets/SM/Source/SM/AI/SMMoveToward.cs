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

      CollisionDirection tempCollisionDirection = _shipPawn.sensorController.checkForCollisions();

      switch (tempCollisionDirection)
      {
        case CollisionDirection.Front:
          _shipPawn.rotateRight(15);
          _shipPawn.moveBackward(-45);
          return TaskStatus.Failure;
        case CollisionDirection.Right:
          _shipPawn.rotateLeft();
          return TaskStatus.Running;
        case CollisionDirection.Left:
          _shipPawn.rotateRight();
          return TaskStatus.Running;
        default:
          break;
      }

      _shipPawn.rotateTowardTarget(currentTarget.Value);
      _shipPawn.moveForward();
      return TaskStatus.Running;
    }
  }
}
