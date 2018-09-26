using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    public class SMLevel : Level
    {
        [Header("Pawns")]
        public List<Pawn> levelPawns = new List<Pawn>();
        public Camera testCam; //cam for setting up the scene and checking stuff
        public SMLevelData levelData;
        public int currentLevelPawnIndex
        {
            get { return _currentLevelPawnIndex; }
            set
            {
                _currentLevelPawnIndex = value;
                // setPawnControllerAndViewByIndex(value);
            }
        }

        protected SMLevelSet currentLoadedLevels;
        protected int _currentLevelPawnIndex;

        protected override void Awake()
        {
            base.Awake();
            init();
            onLevelBegin();
        }

        protected virtual void init()
        {
            SMGame tempGame = (SMGame)game;
            currentLoadedLevels = tempGame.levelManager.currentLoadedLevels;
        }

        protected virtual void onLevelBegin()
        {
            levelName = levelData.levelName;
            SMGame tempGame = (SMGame)game;
            tempGame.setCurrentLevel(levelData);

            //turn off the test camera if it exists
            if (testCam != null)
            {
                testCam.gameObject.SetActive(false);
            }

            if (levelData.levelType == LevelType.Exterior)
            {
                loadExteriorLevel();
            }
            else
            {
                loadInteriorLevel();
            }
        }

        public virtual void loadLevel() { }

        protected virtual void loadInteriorLevel()
        {
            transform.position = levelData.interiorOffset.value;

            //set the current level pawn as the current pawn, if there is one
            if (levelPawns.Count > 0)
            {
                setPawnControllerAndViewByIndex(currentLevelPawnIndex, true);
            }
        }

        protected virtual void loadExteriorLevel()
        {
            SMGame tempGame = Game.GetGame<SMGame>();
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

        // hook for the level manager
        public void reinitializeLevel()
        {
            onLevelBegin();
        }

        // sets it to the current pawn index (for being called outside of the class)
        public virtual void setLevelCurrentPawn()
        {
            game.controller.setCurrentPawn(levelPawns[currentLevelPawnIndex]);
        }

        //for cycling between multiple pawns. if there are multiple
        public virtual void selectNextPawn()
        {
            if (levelPawns.Count > 1)
            {
                currentLevelPawnIndex = currentLevelPawnIndex >= levelPawns.Count - 1 ? 0 : currentLevelPawnIndex + 1;
            }
        }

        public virtual void selectPreviousPawn()
        {
            if (levelPawns.Count > 1)
            {
                currentLevelPawnIndex = currentLevelPawnIndex <= 0 ? levelPawns.Count - 1 : currentLevelPawnIndex - 1;
            }
        }

        protected virtual void setPawnControllerAndViewByIndex(int tPawnIndex, bool isImmediate = false)
        {
            Debug.Log("we set by index");
            game.controller.setCurrentPawn(levelPawns[tPawnIndex]);
            game.view.setTarget(levelPawns[tPawnIndex].transform, isImmediate);
        }

        protected virtual void setPawnControllerAndViewByPawn(SMPawn tPawn)
        {
            Debug.Log("we set by pawn");
            game.controller.setCurrentPawn(tPawn);
            game.view.setTarget(tPawn.transform, true);
        }

        protected virtual void OnEnable()
        {
            currentLoadedLevels.add(this);
        }

        protected virtual void OnDisable()
        {
            currentLoadedLevels.remove(this);
        }
    }
}
