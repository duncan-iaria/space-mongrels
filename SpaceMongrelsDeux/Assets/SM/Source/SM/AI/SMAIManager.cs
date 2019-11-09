using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  public class SMAIManager : MonoBehaviour
  {
    [Tooltip("This is the master GameObject that contains Waypoints as children")]
    public GameObject patrolRoute;

    public List<Transform> patrolWaypoints
    {
      get
      {
        return _patrolWaypoints;
      }
    }

    public List<Transform> _patrolWaypoints;

    void Awake()
    {
      setPatrolRouteWaypoints();
    }

    private void setPatrolRouteWaypoints()
    {
      if (patrolRoute != null)
      {
        foreach (Transform child in patrolRoute.transform)
        {
          _patrolWaypoints.Add(child);
        }
      }
    }
  }

}
