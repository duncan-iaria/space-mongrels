using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    public class SMGame : Game
    {
        [Header("Pawns")]
        public SMPawnShip currentShipPawn;
        public SMPawnMongrel currentMongrelPawn;

        [Header("Levels")]
        public SMLevelManager levelManager;

        protected SMLevelData levelToLoad;

        //=======================
        // Init
        //=======================
        protected override void initialize()
        {
            base.initialize();
            if (levelManager != null)
            {
                levelManager.init(this);
            }
        }

        //=======================
        // Pause
        //=======================
        //actions taken when pause is toggled
        protected override void onTogglePause()
        {
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
                SMLevel currentLevel = levelManager.getCurrentLevel();
                Time.timeScale = 1f;

                //close the main menu visually
                onCloseMainMenu();

                //set controller pawn - not thrilled at how this works
                if (currentLevel.levelPawns.Count > 0)
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
        public virtual void loadLevel(SMLevelData tData, float tTransitionDuration = 0.5f, bool isUsingTrasition = true)
        {
            if (tData != null)
            {
                levelToLoad = tData;

                Invoke("onLoadLevel", tTransitionDuration);
                if (isUsingTrasition)
                {
                    SMGUI tempGUI = GetGUI<SMGUI>();
                    tempGUI.transitionController.startTransition();
                }

                isPaused = false;
            }
        }

        protected virtual void onLoadLevel()
        {
            levelManager.loadLevelByData(levelToLoad);
        }

        public virtual SMLevel getCurrentLevel()
        {
            return levelManager.getCurrentLevel();
        }

        public virtual void setCurrentLevel(SMLevelData tLevelData)
        {
            levelManager.setCurrentLevel(tLevelData);
        }

        protected override void onSceneLoaded(Scene _scene, LoadSceneMode _mode)
        { }

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
