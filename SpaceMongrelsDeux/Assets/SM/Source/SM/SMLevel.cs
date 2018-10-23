using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    // current level flow
    // Awake -> init -> onLevelBegin -> loadLevel
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
            levelName = levelData.levelName;
        }

        protected virtual void onLevelBegin()
        {
            SMGame tempGame = (SMGame)game;
            SMLevel tempPreviousLevel = tempGame.getCurrentLevel();

            tempGame.setCurrentLevel(levelData);

            // turn off the test camera if it exists
            if (testCam != null)
            {
                testCam.gameObject.SetActive(false);
            }

            // for exterior/interior
            loadLevel(tempGame, tempPreviousLevel);
        }

        // stub for children classes
        protected virtual void loadLevel(SMGame tGame, SMLevel tPreviousLevel) { }

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
                setPawnControllerAndViewByIndex(currentLevelPawnIndex);
            }
        }

        public virtual void selectPreviousPawn()
        {
            if (levelPawns.Count > 1)
            {
                currentLevelPawnIndex = currentLevelPawnIndex <= 0 ? levelPawns.Count - 1 : currentLevelPawnIndex - 1;
                setPawnControllerAndViewByIndex(currentLevelPawnIndex);
            }
        }

        protected virtual void setPawnControllerAndViewByIndex(int tPawnIndex, bool isImmediate = false)
        {
            game.controller.setCurrentPawn(levelPawns[tPawnIndex]);
            game.view.setTarget(levelPawns[tPawnIndex].transform, isImmediate);
        }

        protected virtual void setPawnControllerAndViewByPawn(SMPawn tPawn)
        {
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
