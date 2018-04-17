using UnityEngine;
using SNDL;

namespace SM
{
	public class SMInit : SMLevel
	{
		protected override void Start()
		{
			base.Start();

			game.isPaused = true;
			//SMGame tempGame = Game.GetGame<SMGame>();
			//empGame.isPaused = true;
			//splash screen analytics stuff, etc
		}
	}
}
