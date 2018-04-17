using UnityEngine;

namespace SNDL
{
	public class Level : MonoBehaviour
	{
		protected Game game;
		public string levelName;

		protected virtual void Awake()
		{
			game = Game.GetGame<Game>();
		}

		protected virtual void Start()
		{
		}
	}
}
