using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    public class SMLevel : Level
    {
        [Header("Pawns")]
        public Pawn[] levelPawns;
        public Camera testCam; //cam for setting up the scene and checking stuff
        public SMLevelData levelData;
        public int currentLevelPawnIndex
        {
            get { return _currentLevelPawnIndex; }
            set
            {
                _currentLevelPawnIndex = value;
                setPawnControllerAndViewByIndex(value);
            }
        }

        protected int _currentLevelPawnIndex;

        protected override void Awake()
        {
            base.Awake();
            onLevelBegin();
        }

        protected virtual void onLevelBegin()
        {
            SMGame tempGame = Game.GetGame<SMGame>();
            //turn off the test camera if it exists
            if (testCam != null)
            {
                testCam.gameObject.SetActive(false);
            }

            //if it's not an additive level, set this as the current level
            if (!levelData.isAdditiveLevel)
            {
                tempGame.currentLevel = this;
            }

            if (levelData.levelType == LevelType.Exterior)
            {
                Debug.Log("HEEEY SM RACEWAY");
                SMPawnShip tempPawn = Instantiate(tempGame.currentShipPawn) as SMPawnShip;
                setPawnControllerAndViewByPawn(tempPawn);
            }
            else
            {
                //set the current level pawn as the current pawn, if there is one
                if (levelPawns.Length > 0)
                {
                    setPawnControllerAndViewByIndex(currentLevelPawnIndex);
                }
            }
        }

        //sets it to the current pawn index (for being called outside of the class)
        public virtual void setLevelCurrentPawn()
        {
            game.controller.setCurrentPawn(levelPawns[currentLevelPawnIndex]);
        }

        //for cycling between multiple pawns. if there are multiple
        public virtual void selectNextPawn()
        {
            if (levelPawns.Length > 1)
            {
                currentLevelPawnIndex = currentLevelPawnIndex >= levelPawns.Length - 1 ? 0 : currentLevelPawnIndex + 1;
            }
        }

        public virtual void selectPreviousPawn()
        {
            if (levelPawns.Length > 1)
            {
                currentLevelPawnIndex = currentLevelPawnIndex <= 0 ? levelPawns.Length - 1 : currentLevelPawnIndex - 1;
            }
        }

        protected virtual void setPawnControllerAndViewByIndex(int tPawnIndex)
        {
            game.controller.setCurrentPawn(levelPawns[tPawnIndex]);
            game.view.setTarget(levelPawns[tPawnIndex].transform);
        }

        protected virtual void setPawnControllerAndViewByPawn(SMPawn tPawn)
        {
            game.controller.setCurrentPawn(tPawn);
            game.view.setTarget(tPawn.transform);
        }

        protected virtual void OnEnable()
        {
            SceneManager.activeSceneChanged += onSceneChange;
        }

        protected virtual void OnDisable()
        {
            SceneManager.activeSceneChanged -= onSceneChange;
        }

        protected virtual void onSceneChange(Scene tCurrent, Scene tNext)
        {
            Debug.Log("next scene :" + tNext.name);
            // if it's an interior level and THIS interior
            if (tNext.name == levelData.levelName)
            {
                Debug.Log("ON BEGIN CALLED ON :" + tNext.name);
                onLevelBegin();
            }
        }
    }
}
