﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    public class SMTycho : SMLevel
    {
        protected override void onLevelBegin()
        {
            base.onLevelBegin();
            // game.view.setTarget(levelPawns[currentLevelPawnIndex].transform, true);
        }
    }
}