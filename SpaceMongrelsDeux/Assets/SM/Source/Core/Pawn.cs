using UnityEngine;

namespace SNDL
{
	public class Pawn : MonoBehaviour
	{
		public bool isCurrentPawn
		{
			get{ return _isCurrentPawn; }
			set{ _isCurrentPawn = value; onSetCurrentPawn(); }
		}

		protected bool _isCurrentPawn = false;

		//=========================
		// Initialization
		//=========================
		protected virtual void Awake()
		{ }

		//=========================
		// Pawn Set/Unset Actions
		//=========================
		//method for determining if it was set or unset - automatically called when it changes
		protected virtual void onSetCurrentPawn()
		{
			if( _isCurrentPawn )
			{
				onPawnSet();
			}
			else
			{
				onPawnUnset();
			}
		}

		//actions taken when pawn is set
		public virtual void onPawnSet()
		{ }

		//actions taken when pawn is unset
		public virtual void onPawnUnset()
		{ }

		//=========================
		// Input
		//=========================
		public virtual void onInputButton( InputButton tButton )
		{
			//switch statement with all possible button input types
		}

		public virtual void onAxis( InputAxis tAxis, float tValue )
		{ }


		public virtual void onJump()
		{ }

		public virtual void onJump( float jumpStr )
		{ }

		public virtual void onCancel()
		{ }

		public virtual void onPause()
		{
		}

		public virtual void onLeftAxis()
		{ }

		public virtual void onLeftAxis( Vector2 _axis )
		{ }
	}
}

