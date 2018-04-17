using UnityEngine;
using UnityEngine.Events;

namespace SM
{
	public class SMTransition : MonoBehaviour
	{
		[Header( "Components" )]
		public GameObject loadingIcon;
		protected Animator animator;

		[Header( "Properties" )]
		[Tooltip( "How long the transition will stay 'open' in the middle" )]
		public float transitionTimeoutDuration = .2f;

		[Header( "Events" )]
		public UnityEvent onTransitionComplete;

		protected int _state = Animator.StringToHash( "gateState" );
		protected int _transitionTrigger = Animator.StringToHash( "transition" );

		//====================
		// Initialize
		//=====================
		protected virtual void Awake()
		{
			animator = GetComponent<Animator>();
		}

		//====================
		// Transitions
		//=====================
		//for transitioning to the loading screen
		public void startTransition()
		{
			if( animator != null )
			{
				animator.SetInteger( _state, (int)GateState.Opening );
				animator.SetTrigger( _transitionTrigger );
			}
		}

		//when the transition has completely "opened" (it is in the middle)
		public void completeTransition()
		{
			//onComplete event
			if( onTransitionComplete != null )
			{
				onTransitionComplete.Invoke();
			}

			//end event
			Invoke( "endTransition", transitionTimeoutDuration );
		}


		//for returning to the game
		public void endTransition()
		{
			if( animator != null )
			{
				animator.SetInteger( _state, (int)GateState.Closing );
				animator.SetTrigger( _transitionTrigger );
			}
		}

		//====================
		// Loading Icon
		//=====================
		//loading icon hooks for animation events
		public void enableLoadingIcon()
		{
			if( loadingIcon != null )
			{
				loadingIcon.SetActive( true );
			}
		}

		public void disableLoadingIcon()
		{
			if( loadingIcon != null )
			{
				loadingIcon.SetActive( false );
			}
		}
	}
}