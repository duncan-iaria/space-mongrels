using UnityEngine;
using UnityEngine.Events;

namespace SM
{
    [System.Serializable]
    public class NotificationUnityEvent : UnityEvent<string, Vector3> { }

    public class NotificationEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public NotificationEvent notificationEvent;

        [Tooltip("Response to invoke when Event is raised.")]
        public NotificationUnityEvent response;

        private void OnEnable()
        {
            notificationEvent.registerListener(this);
        }

        private void OnDisable()
        {
            notificationEvent.unregisterListener(this);
        }

        public void onEventRaised(string tMessage, Vector3 tTargetPosition)
        {
            if (response != null)
            {
                response.Invoke(tMessage, tTargetPosition);
            }
            else
            {
                Debug.Log("Event was null, could not invoke");
            }
        }
    }
}