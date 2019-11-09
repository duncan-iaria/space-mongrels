using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  public class SMLevelExterior : SMLevel
  {
    [Header("Locations")]
    public Transform defaultSpawnPoint;
    protected List<SMLevelGate> spawnPoints = new List<SMLevelGate>();

    [Header("Level Gates")]
    public Transform gates;

    protected override void init()
    {
      base.init();
      getAllPossibleSpawnPoints();
    }

    // Get all spawn points from the master node and
    // Add them to the existing spawn point list
    private void getAllPossibleSpawnPoints()
    {
      if (gates != null)
      {
        SMLevelGate[] tempGates = gates.GetComponentsInChildren<SMLevelGate>();
        if (tempGates.Length > 0)
        {
          foreach (SMLevelGate tempGate in tempGates)
          {
            spawnPoints.Add(tempGate);
          }
        }
      }
    }

    protected override void loadLevel(SMGame tGame, SMLevel tPreviousLevel)
    {
      if (levelPawns.Count > 0)
      {
        currentLevelPawnIndex = 0;
        setPawnControllerAndViewByIndex(currentLevelPawnIndex, true);
      }
      else
      {
        SMPawnShip tempPawn = Instantiate(tGame.currentShipPawn, getSpawnPoint(tPreviousLevel), Quaternion.identity) as SMPawnShip;
        tempPawn.transform.parent = this.transform;

        setPawnControllerAndViewByPawn(tempPawn);
        // boost away
        // tempPawn.onInputButton(InputButton.Boost);
        levelPawns.Add(tempPawn);
        currentLevelPawnIndex = 0;
      }
    }

    protected Vector3 getSpawnPoint(SMLevel tPreviousLevel)
    {
      if (spawnPoints.Count > 0 && tPreviousLevel != null)
      {
        for (int i = spawnPoints.Count - 1; i >= 0; --i)
        {
          if (spawnPoints[i].sourceLevel.levelName == tPreviousLevel.levelName)
          {
            return spawnPoints[i].transform.position;
          }
        }
      }

      return defaultSpawnPoint != null ? defaultSpawnPoint.position : new Vector3();
    }
  }
}