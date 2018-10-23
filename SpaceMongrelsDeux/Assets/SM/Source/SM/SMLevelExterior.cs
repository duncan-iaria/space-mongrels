using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNDL;

namespace SM
{
    public class SMLevelExterior : SMLevel
    {
        public SMLevelGate[] spawnPoints;

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
            if (spawnPoints.Length > 0 && tPreviousLevel != null)
            {
                for (int i = spawnPoints.Length - 1; i >= 0; --i)
                {
                    if (spawnPoints[i].sourceLevel.levelName == tPreviousLevel.levelName)
                    {
                        return spawnPoints[i].transform.position;
                    }
                }
            }

            return new Vector3();
        }
    }
}