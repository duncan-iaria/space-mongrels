using UnityEngine;

namespace SM
{
  public class SMNotifier : MonoBehaviour
  {
    public string message;
    public Transform notificationLocation;
    public NotificationEvent notificationEvent;

    public void setMessage(string tMessage)
    {
      message = tMessage;
    }

    public void setNofiticationEvent(NotificationEvent tEvent)
    {
      notificationEvent = tEvent;
    }

    public void sendNotification()
    {
      Vector3 tempPos = notificationLocation != null ? notificationLocation.position : transform.position;
      notificationEvent.raise(message, tempPos);
    }
  }
}
