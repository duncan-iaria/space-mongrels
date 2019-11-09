using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace SM
{
  // THIS IS FOR INITIALIZING ALL AI SHIPS
  // WILL GATHER ALL THE NECESSARY STATS FOR
  // THE SUBSEQUENT BEHAVIOUR TREE ACTIONS/CONDITIONS
  // AS THEY RELATE TO THE SHIP ITSELF (SPEED/RANGE, ETC)
  public class SMInitilizeEnemyShipAI : Action
  {
    public SharedFloat sensorRange;
    public SharedFloat moveSpeed;
    public SharedFloat rotationSpeed;
    public SharedTransformList levelPawns;
    public SharedTransformList patrolWaypoints;
    public override void OnStart()
    {
      SMPawnShip tempPawnShip = this.transform.GetComponent<SMPawnShip>();

      if (tempPawnShip != null)
      {
        sensorRange.Value = tempPawnShip.sensorController.range;
        moveSpeed.Value = tempPawnShip.moveSpeed;
        rotationSpeed.Value = tempPawnShip.rotationSpeed;
      }

      levelPawns.Value = getLevelPawns();
      patrolWaypoints.Value = getLevelPatrolWaypoints();

    }

    private List<Transform> getLevelPawns()
    {
      SMLevel tempLevel = getCurrentLevel();


      if (tempLevel != null)
      {
        List<Transform> tempLevelPawnTransforms = new List<Transform>();

        for (int i = tempLevel.levelPawns.Count - 1; i >= 0; --i)
        {
          tempLevelPawnTransforms.Add(tempLevel.levelPawns[i].transform);
        }

        return tempLevelPawnTransforms;

      }

      Debug.LogWarning("NO SMGAME FOUND IN AI INITIALIZATION — THIS IS MOST LIKELY A PROBLEM");

      return new List<Transform>();
    }

    private List<Transform> getLevelPatrolWaypoints()
    {
      SMLevel tempLevel = getCurrentLevel();

      if (tempLevel != null)
      {
        return tempLevel.AIManager.patrolWaypoints;
      }
      return new List<Transform>();
    }

    private SMLevel getCurrentLevel()
    {
      SMGame tempGame = SMGame.GetGame<SMGame>();

      if (tempGame != null)
      {
        return tempGame.getCurrentLevel();
      }
      else return null;
    }

    public override TaskStatus OnUpdate()
    {
      return TaskStatus.Success;
    }
  }
}
