using UnityEngine;
using UnityEngine.Events;

namespace SM
{
    public class SMInteractable : MonoBehaviour
    {
        public bool isEntryTrigger;
        public UnityEvent onTriggerEntry;

        public bool isExitTrigger;
        public UnityEvent onTriggerExit;

        public UnityEvent onInteractEvent;

        public int levelToLoad;
        public float loadTransitionDuration;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            //if its a pawn, set as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                tempPawn.currentInteractable = this;
            }

            //invoke event
            if (isEntryTrigger && onTriggerEntry != null)
            {
                onTriggerEntry.Invoke();
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            //clear the current interactvable of the pawn
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                tempPawn.currentInteractable = null;
            }

            //invoke event
            if (isExitTrigger && onTriggerExit != null)
            {
                onTriggerExit.Invoke();
            }
        }

        //event that happnens when interacted with 
        //Must be set in inpsector, CORE of how this component works
        //Select a function from the Possible list before
        public virtual void onInteract()
        {
            Debug.Log("on interact received");
            if (onInteractEvent != null)
            {
                onInteractEvent.Invoke();
            }
        }

        //POSSIBLE INTERACTABLE ACTIONS
        public virtual void onLoadLevel()
        {
            SMGame tempGame = SMGame.GetGame<SMGame>();
            tempGame.onLoadLevel(levelToLoad, loadTransitionDuration, true);
        }
    }
}

