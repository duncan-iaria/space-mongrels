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

        public UnityEvent onTriggerDeselect;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            //if its a pawn, set as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                if (tempPawn.currentInteractableObject)
                {
                    tempPawn.currentInteractableObject.onDeselect();
                }

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
            //if its a pawn, unset as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                if (tempPawn.currentInteractable != null && tempPawn.currentInteractable == this)
                {
                    tempPawn.currentInteractableObject = null;
                }
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

        //If another object becomes selected, deselect this one
        public void onDeselect()
        {
            interactable.onDeselect();
            if (onTriggerDeselect != null)
            {
                onTriggerDeselect.Invoke();
            }
        }

        //event that happnens when interacted with
        public virtual void onInteract()
        {
            interactable.onInteract();
        }
    }
}
