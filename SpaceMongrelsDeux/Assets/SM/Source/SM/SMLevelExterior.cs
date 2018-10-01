using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{

    public class SMLevelExterior : SMLevel
    {
        protected override void loadLevel(SMGame tGame)
        {
            if (levelPawns.Count > 0)
            {
                currentLevelPawnIndex = 0;
                setPawnControllerAndViewByIndex(currentLevelPawnIndex, true);

            }
            else
            {
                SMPawnShip tempPawn = Instantiate(tGame.currentShipPawn) as SMPawnShip;
                setPawnControllerAndViewByPawn(tempPawn);
                levelPawns.Add(tempPawn);
                currentLevelPawnIndex = 0;
            }
        }
    }
}