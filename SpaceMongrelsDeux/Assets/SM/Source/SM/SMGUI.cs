using UnityEngine;
using UnityEngine.SceneManagement;
using SNDL;

namespace SM
{
	public class SMGUI : SNDL.GUI
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

			if( mainMenu.activeSelf )
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
			mainMenu.SetActive( true );
		}

		public override void onCloseMainMenu()
		{
			base.onCloseMainMenu();
			mainMenu.SetActive( false );
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
		public override void onLoadScene( int _sceneIndex )
		{
			SMGame.GetGame<SMGame>().onLoadLevel( _sceneIndex, transitionDuration );
			transitionController.startTransition();
		}

		//End scene loading - triggered by event from SceneManagement
		protected override void onSceneLoaded( Scene _scene, LoadSceneMode _mode )
		{
			////when a scene was loaded, assuming it's not an additive scene, end the transition and hide the menu
			//if( _mode != LoadSceneMode.Additive )
			//{
			//	//if it's the initial scene
			//	if( _scene.buildIndex == 0 )
			//	{
			//		onOpenMainMenu();
			//		SMGame.instance.controller.setCurrentPawn( menuPawn );
			//	}
			//	//if it's any other scene
			//	else
			//	{
			//		transitionController.endTransition();
			//	}
			//}
		}
	}
}
