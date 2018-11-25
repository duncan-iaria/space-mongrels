using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    [CreateAssetMenu]
    public class NotificationEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<NotificationEventListener> eventListeners =
            new List<NotificationEventListener>();

        public void raise(string tMessage, Vector3 tTargetPosition)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].onEventRaised(tMessage, tTargetPosition);
        }

        public void registerListener(NotificationEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void unregisterListener(NotificationEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}