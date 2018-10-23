using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Interactables/EventTrigger", order = 100)]
    public class SMEventInteractable : Interactable
    {
        public UnityEvent eventToTrigger;

        public override void onInteract()
        {
            if (eventToTrigger != null)
            {
                eventToTrigger.Invoke();
            }
        }

        public override void onEnter() { }
        public override void onExit() { }
        public override void onDeselect() { }
    }
}
