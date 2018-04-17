using UnityEngine;

namespace SNDL
{
	//#######################
	// Controller Class
	//#######################
	public class Controller : MonoBehaviour
	{
		[Header( "Components" )]
		public Pawn currentPawn;

		void Start()
		{
			if( currentPawn != null )
			{
				currentPawn.isCurrentPawn = true;
			}
		}

		//=======================
		// Pawn Assignment
		//=======================
		public virtual void setCurrentPawn( Pawn tPawn )
		{
			//if there is already an active pawn
			if( currentPawn != null )
			{
				//calls unset actions
				currentPawn.onPawnUnset();

				//make sure it's no longer active pawn
				currentPawn.isCurrentPawn = false;
			}

			//set current pawn to new pawn
			currentPawn = tPawn;

			//if the pawn WAS assigned to something
			if( currentPawn != null )
			{
				//calls set actions
				currentPawn.onPawnSet();

				currentPawn.isCurrentPawn = true;
			}
		}

		//=======================
		// Pawn Controls
		//=======================
		public virtual void onInputButton( InputButton tButton )
		{
        }

		public virtual void onAxis( InputAxis tAxis, float tValue )
		{
		}

		//=======================
		// Pawn Controls
		//=======================
		public virtual void onPressCancel()
		{
		}

		public virtual void onPressPause()
		{
		}

		public virtual void onPressCycle()
		{
		}
	}
}
