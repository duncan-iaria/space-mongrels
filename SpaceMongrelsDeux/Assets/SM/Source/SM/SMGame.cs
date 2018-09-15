using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    public class SMGame : Game
    {
        public SMLevel currentLevel;

        //Loading
        public int levelToLoad;
        protected int currentLevelIndex;
        protected string levelToLoadName;

        //=======================
        // Pause
        //=======================
        //actions taken when pause is toggled
        protected override void onTogglePause()
        {
            Debug.Log("we here");
            if (isPaused)
            {
                Time.timeScale = 0f;

                //open the main menu visually
                onOpenMainMenu();

                //set controller pawn
                controller.setCurrentPawn(GetGUI<SMGUI>().menuPawn);

                //log game state
                Debug.Log("IS PAUSED!");
            }
            else
            {
                Time.timeScale = 1f;

                //close the main menu visually
                onCloseMainMenu();

                //set controller pawn - not thrilled at how this works
                if (currentLevel.levelPawns.Length > 0)
                {
                    controller.setCurrentPawn(currentLevel.levelPawns[currentLevel.currentLevelPawnIndex]);
                }

                //log game state
                Debug.Log("IS UNPAUSED!");
            }
        }

        //=======================
        // Level Loading
        //=======================
        public virtual void onLoadLevel(int tLevelIndex, float tTransitionDuration = 0.5f, bool isUsingTrasition = true)
        {
            //set the level to be loaded next(because we can't set with invoke)
            levelToLoad = tLevelIndex;

            //game load level function to execute when the closing transition is complete
            Invoke("loadLevel", tTransitionDuration);

            if (isUsingTrasition)
            {
                SMGUI tempGUI = GetGUI<SMGUI>();
                tempGUI.transitionController.startTransition();
            }

            //unpause game (so it can load)
            isPaused = false;
        }

        public virtual void onLoadLevelByName(string tLevelName, float tTransitionDuration = 0.5f, bool isUsingTrasition = true)
        {
            levelToLoadName = tLevelName;
            Invoke("loadLevelByName", tTransitionDuration);

            if (isUsingTrasition)
            {
                SMGUI tempGUI = GetGUI<SMGUI>();
                tempGUI.transitionController.startTransition();
            }

            //unpause game (so it can load)
            isPaused = false;
        }

        protected virtual void loadLevel()
        {
            SceneManager.LoadScene(levelToLoad);
        }

        protected virtual void loadLevelByName()
        {
            SceneManager.LoadScene(levelToLoadName);
        }


        protected override void onSceneLoaded(Scene _scene, LoadSceneMode _mode)
        {
            currentLevelIndex = levelToLoad;
        }

        //=======================
        // GUI Controls
        //=======================
        public void onOpenMainMenu()
        {
            gameGUI.onOpenMainMenu();
        }

        public void onCloseMainMenu()
        {
            gameGUI.onCloseMainMenu();
        }

        public void onToggleMainMenu()
        {
            gameGUI.onTogglMainMenu();
        }
    }
}
