using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SNDL;

namespace SM
{
    public class SMInteractable : MonoBehaviour
    {
        public Interactable interactable;

        [Header("Entry")]
        public bool isEntryTrigger;
        public UnityEvent onTriggerEntry;
        public bool isNotification;
        public string notificationMessage;
        public NotificationEvent onNotification;

        [Header("Exit")]
        public bool isExitTrigger;
        public UnityEvent onTriggerExit;
        public NotificationEvent onDeactivateNotification;

        [Header("Deselect")]
        public UnityEvent onTriggerDeselect;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            //if its a pawn, set as current interactable
            SMPawn tempPawn = other.GetComponent<SMPawn>();

            if (tempPawn != null)
            {
                if (tempPawn.currentInteractable != null)
                {
                    tempPawn.currentInteractable.onDeselect();
                }

                tempPawn.currentInteractable = this;
            }

            //invoke event
            if (isEntryTrigger)
            {
                interactable.onEnter();
                if (onTriggerEntry != null)
                {
                    onTriggerEntry.Invoke();
                }

                if (isNotification && onNotification != null)
                {
                    onNotification.raise(notificationMessage, this.transform.position);
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
                    tempPawn.currentInteractable = null;
                }
            }

            if (isExitTrigger)
            {
                interactable.onExit();
                if (onTriggerExit != null)
                {
                    onTriggerExit.Invoke();
                }

                if (isNotification && onDeactivateNotification != null)
                {
                    onDeactivateNotification.raise("", Vector3.zero);
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
