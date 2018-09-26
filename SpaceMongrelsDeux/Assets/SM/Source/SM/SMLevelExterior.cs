using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{

    public class SMLevelExterior : SMLevel
    {
        protected virtual void loadLevel()
        {
            Debug.Log("loading exterior");

            SMGame tempGame = (SMGame)game;

            if (levelPawns.Count > 0)
            {
                currentLevelPawnIndex = 0;
                setPawnControllerAndViewByIndex(currentLevelPawnIndex, true);

            }
            else
            {
                SMPawnShip tempPawn = Instantiate(tempGame.currentShipPawn) as SMPawnShip;
                setPawnControllerAndViewByPawn(tempPawn);
                levelPawns.Add(tempPawn);
                currentLevelPawnIndex = 0;
            }
        }
    }
}