using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
    public class SMGUI : SNDL.GameGUI
    {
        [Header("GUI Elements")]
        public GameObject mainMenu;
        public SMPawnMenu menuPawn;
        public SMTransition transitionController;
        public float transitionDuration = .5f;

        protected Animator animator;

        //=======================
        // Initialize
        //=======================
        protected virtual void Awake()
        {
            //get the animator
            animator = GetComponent<Animator>();
        }

        //=======================
        // Menu Controls
        //=======================
        public override void onTogglMainMenu()
        {
            base.onTogglMainMenu();

            if (mainMenu.activeSelf)
            {
                //if it's already active, close it
                onCloseMainMenu();
            }
            else
            {
                //open it
                onOpenMainMenu();
            }
        }
        public override void onOpenMainMenu()
        {
            base.onOpenMainMenu();
            mainMenu.SetActive(true);
        }

        public override void onCloseMainMenu()
        {
            base.onCloseMainMenu();
            mainMenu.SetActive(false);
        }

        public virtual void onQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
        }

        //=======================
        // Scene Loading
        //=======================
        //Start Scene Loading - GUI directly called from main menu
        public void onPressStart(SMLevelData tLevelData)
        {
            SMGame.GetGame<SMGame>().loadLevel(tLevelData, transitionDuration);
        }

        public void EventTest()
        {
            Debug.Log("Test Event Has Fired");
        }
    }
}
