using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace SM
{

  public class SMGetClosestTransform : Action
  {
    [BehaviorDesigner.Runtime.Tasks.Tooltip("List of potnetial targets to be compared")]
    public SharedTransformList potentialTargets;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Result of comparison gets set as the current target")]
    public SharedTransform currentTarget;

    public override void OnStart()
    {
      base.OnStart();
      currentTarget.Value = getClosestTransform();
    }

    Transform getClosestTransform()
    {
      Transform tempClosestTransform = null;
      float closestDistanceSqr = Mathf.Infinity;
      Vector3 currentPosition = transform.position;

      foreach (Transform tempTarget in potentialTargets.Value)
      {
        Vector3 directionToTarget = tempTarget.position - currentPosition;
        float dSqrToTarget = directionToTarget.sqrMagnitude;
        if (dSqrToTarget < closestDistanceSqr)
        {
          closestDistanceSqr = dSqrToTarget;
          tempClosestTransform = tempTarget;
        }
      }

      return tempClosestTransform;
    }
    public override TaskStatus OnUpdate()
    {
      base.OnUpdate();
      return TaskStatus.Success;
    }
  }
}
