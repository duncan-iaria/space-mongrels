using UnityEngine;
using SNDL;

namespace SM
{
	public class SMLevel : Level
	{
		[Header("Pawns")]
		public Pawn[] levelPawns;
		public int currentLevelPawnIndex;

		public Camera testCam; //cam for setting up the scene and checking stuff
		public bool isAdditiveLevel = false;

		protected override void Awake()
		{
			base.Awake();
			onLevelBegin();
		}

		protected virtual void onLevelBegin()
		{
			//turn off the test camera if it exists
			if( testCam != null )
			{
				testCam.gameObject.SetActive( false );
			}

			//if it's not an additive level, set this as the current level
			if( !isAdditiveLevel )
			{
				Game.GetGame<SMGame>().currentLevel = this;
			}

			//set the current level pawn as the current pawn, if there is one
			if( levelPawns.Length > 0 )
			{
				game.controller.setCurrentPawn( levelPawns[currentLevelPawnIndex] );

				//set the view
				game.view.setTarget( levelPawns[currentLevelPawnIndex].transform );
			}
		}

		//sets it to the current pawn index (for being called outside of the class)
		public virtual void setLevelCurrentPawn()
		{
			game.controller.setCurrentPawn( levelPawns[currentLevelPawnIndex] );
		}

		//for cycling between multiple pawns. if there are multiple
		public virtual void cyclePawns()
		{
			//if there is more than one pawn - otherwise don't waste the cycles
			if( levelPawns.Length > 1 )
			{
				//if its the last pawn in the list
				if( currentLevelPawnIndex >= levelPawns.Length - 1 )
				{
					//reset the index at 0
					currentLevelPawnIndex = 0;
				}
				else
				{
					//increment pawn index
					++currentLevelPawnIndex;
				}

				//set the pawn
				game.controller.setCurrentPawn( levelPawns[currentLevelPawnIndex] );

				//set the view
				game.view.setTarget( levelPawns[currentLevelPawnIndex].transform );
			}
		}

	}
}
