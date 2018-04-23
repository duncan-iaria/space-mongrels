using UnityEngine;
using SNDL;

namespace SM
{
    //#######################
    // SM Controller Class
    //#######################
    public class SMController : Controller
    {
        public Pawn previousPawn = null;

        //=======================
        // Pawn Assignments
        //=======================
        //public virtual void setCurrentPawnAsPrevious()
        //{
        //	currentPawn = previousPawn;
        //}

        //=======================
        // Pawn Controls
        //=======================
        public override void onInputButton(InputButton tButton)
        {
            //forward the button to the current pawn
            if (currentPawn != null)
            {
                currentPawn.onInputButton(tButton);
            }
            else
            {
                Debug.LogWarning("input was not given to pawn, as no pawn is assigned!");
            }
        }

        public override void onAxis(InputAxis tAxis, float tValue)
        {
            currentPawn.onAxis(tAxis, tValue);
        }

        //=======================
        // Global Controls
        //=======================
        //on pressing a cancel command
        public override void onPressCancel()
        {
            if (currentPawn == null)
            {
                Debug.Log("pressed cancel");
                Game.instance.togglePause();
            }
            else
            {
                currentPawn.onCancel();
            }
        }

        //on press the start button (pause button)
        public override void onPressPause()
        {
            if (currentPawn == null)
            {
                Debug.Log("pressed pause");
                Game.instance.togglePause();
            }
            else
            {
                currentPawn.onPause();
            }
        }

        public override void onPressCycle()
        {
            //currentPawn.onPressedCycle();
            cyclePawns();
        }

        //cycle possible pawns (based on the level)
        public virtual void cyclePawns()
        {
            Game.GetGame<SMGame>().currentLevel.cyclePawns();
        }
    }
}
