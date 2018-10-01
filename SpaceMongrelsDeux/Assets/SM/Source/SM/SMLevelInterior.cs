using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{

    public class SMLevelInterior : SMLevel
    {
        protected override void loadLevel(SMGame tGame)
        {
            transform.position = levelData.interiorOffset.value;

            // set the current level pawn as the current pawn, if there is one
            if (levelPawns != null)
            {
                int tempPawnIndex = levelPawns.Count > 0 ? currentLevelPawnIndex : 0;
                setPawnControllerAndViewByIndex(tempPawnIndex, true);
            }
        }

    }
}