using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SM
{
    public class SMInteractableObject : MonoBehaviour
    {
        public SMInteractable interactable;

        public bool isEntryTrigger;
        public UnityEvent onTriggerEntry;

        public bool isExitTrigger;
        public UnityEvent onTriggerExit;

        // public UnityEvent onInteractEvent;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            //if its a pawn, set as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                tempPawn.currentInteractableObject = this;
            }

            //invoke event
            if (isEntryTrigger)
            {
                interactable.onEnter();
                if (onTriggerEntry != null)
                {
                    onTriggerEntry.Invoke();
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            //if its a pawn, set as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                tempPawn.currentInteractableObject = null;
            }

            if (isExitTrigger)
            {
                interactable.onExit();
                if (onTriggerExit != null)
                {
                    onTriggerExit.Invoke();
                }
            }
        }

        //event that happnens when interacted with 
        //Must be set in inpsector, CORE of how this component works
        //Select a function from the Possible list before
        public virtual void onInteract()
        {
            interactable.onInteract();
        }
    }
}
