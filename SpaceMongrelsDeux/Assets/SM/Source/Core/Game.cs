using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace SNDL
{
	public class Game : MonoBehaviour
	{
		[Header( "Properties" )]
		public string gameName = "Space Mongrels";
		public bool isPaused
		{
			get { return _isPaused; }
			set { _isPaused = value; onTogglePause(); }
		}

		protected bool _isPaused = false;

		[Header( "Components" )]
		public GUI GUI;
		public View view;
		public Controller controller;
		public Inputter inputter;

		//protected GMGame gamePrefab;
		protected static Game _instance;

		//=======================
		// Singleton
		//=======================
		public static Game instance
		{
			get
			{
				if( _instance == null )
				{
					//_instance = Instantiate( gamePrefab ) as GMGame;
					GameObject tempGame = Instantiate( Resources.Load( "Game" ) as GameObject );
					_instance = tempGame.GetComponent<Game>();
					_instance.initialize();
				}
				return _instance;
			}
		}

		//get a refernce to the current game instance (as any kind of game)
		public static T GetGame<T>() where T : Game
		{
			return ReferenceEquals( instance, null ) ? null : instance as T;
		}

		//=======================
		// Initialization
		//=======================
		protected virtual void initialize()
		{
			DontDestroyOnLoad( gameObject );
			_instance.name = gameName;

			//load GUI scene and other required scenes
			initGUI();

			//load View and set it up
			initView();
		}

		//GUI INIT
		protected virtual void initGUI()
		{
			if( GUI != null )
			{
				GUI tempGUI = Instantiate( GUI ) as GUI;
				DontDestroyOnLoad( tempGUI );
				tempGUI.name = "GUI";

				//update the reference to the spawned GUI - TODO: (is this bad)?
				GUI = tempGUI;
			}
			else
			{
				Debug.LogWarning( "NO GUI OBJECT HAS BEEN ASSIGNED!" );
			}
		}

		//VIEW INIT
		protected virtual void initView()
		{
			if( view != null )
			{
				View tempView = Instantiate( view ) as View;
				DontDestroyOnLoad( tempView );
				tempView.name = "View";

				//update the reference to the spawned view TODO: (is this bad)?
				view = tempView;
			}
			else
			{
				Debug.LogWarning( "NO VIEW OBJECT HAS BEEN ASSIGNED!" );
			}
		}

		//check to make sure the game doesn't already exist
		protected void Awake()
		{
			if( _instance != null )
			{
				Destroy( gameObject );
				Debug.LogWarning( "There can be only one instance of Game!" );
			}
		}

		//=======================
		// Component Hooks
		//=======================
		//for getting getting key components
		//get the GUI
		public T GetGUI<T>() where T : GUI
		{
			if( GUI != null )
			{
				return GUI as T;
			}
			else
			{
				Debug.LogWarning( "COULD NOT GET GUI AS ONE DOES NOT EXIST" );
				return null;
			}
		}

		//get the view
		public T GetView<T>() where T : View
        {
			if( Game.instance.view != null )
			{
				return instance.view as T;
			}
			else
			{
				Debug.LogWarning( "COULD NOT GET VIEW AS NO VIEW EXISTS (IS ASSIGNED)" );
				return null;
			}
		}

		//=======================
		// Level Loading
		//=======================
		protected virtual void onSceneLoaded( Scene _scene, LoadSceneMode _mode )
		{
		}

		//=======================
		// Pause
		//=======================
		public virtual void togglePause()
		{
			if( isPaused )
			{
				isPaused = false;
			}
			else
			{
				isPaused = true;
			}
		}

		//actions taken when pause is toggled
		protected virtual void onTogglePause()
		{
		}

		//=======================
		// Event Subscription
		//=======================
		protected void OnEnable()
		{
			SceneManager.sceneLoaded += onSceneLoaded;
		}

		protected void OnDisable()
		{
			SceneManager.sceneLoaded -= onSceneLoaded;
		}
	}
}
