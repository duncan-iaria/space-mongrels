using UnityEngine;
using UnityEngine.SceneManagement;

namespace SNDL
{
	public class GUI : MonoBehaviour
	{
		//=======================
		// Main Menu
		//=======================
		public virtual void onTogglMainMenu()
		{
		}

		public virtual void onOpenMainMenu()
		{
		}

		public virtual void onCloseMainMenu()
		{
		}

		//=======================
		// Scene Loading
		//=======================
		public virtual void onLoadScene( int _sceneIndex )
		{
		}

		protected virtual void onSceneLoaded( Scene _scene, LoadSceneMode _mode )
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

