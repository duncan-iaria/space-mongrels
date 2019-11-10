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
    private SMPawnShip shipPawn;


    public override void OnStart()
    {
      base.OnStart();
      shipPawn = this.transform.GetComponent<SMPawnShip>();
      if (shipPawn == null)
      {
        Debug.LogWarning("NO PAWN FOUND FOR AI");
      }
    }

    public override TaskStatus OnUpdate()
    {
      // Return a task status of success once we've reached the target
      if (Vector3.SqrMagnitude(transform.position - currentTarget.Value.position) < successDistance)
      {
        return TaskStatus.Success;
      }

      shipPawn.rotateTowardTarget(currentTarget.Value);
      shipPawn.moveForward();
      return TaskStatus.Running;
    }
  }
}
