using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class SMGotoNextWaypoint : Action
{
  public SharedTransform currentTarget;
  public SharedTransformList waypoints;

  public override void OnStart()
  {
    if (waypoints.Value != null)
    {
      int tempIndex = waypoints.Value.IndexOf(currentTarget.Value);

      if (tempIndex >= waypoints.Value.Count - 1)
      {
        currentTarget.Value = waypoints.Value[0];
      }
      else
      {
        currentTarget.Value = waypoints.Value[tempIndex + 1];
      }
    }
  }

  public override TaskStatus OnUpdate()
  {
    return TaskStatus.Success;
  }
}
