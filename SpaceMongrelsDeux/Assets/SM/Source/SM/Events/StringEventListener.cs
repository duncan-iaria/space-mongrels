using UnityEngine;
using UnityEngine.Events;

namespace SNDL
{
  [System.Serializable]
  public class StringUnityEvent : UnityEvent<string> { }

  public class StringEventListener : MonoBehaviour
  {
    [Tooltip("Event to register with.")]
    public StringEvent stringEvent;

    [Tooltip("Response to invoke when Event is raised.")]
    public StringUnityEvent response;

    private void OnEnable()
    {
      stringEvent.registerListener(this);
    }

    private void OnDisable()
    {
      stringEvent.unregisterListener(this);
    }

    public void onEventRaised(string tString)
    {
      if (response != null)
      {
        response.Invoke(tString);
      }
      else
      {
        Debug.Log("Event was null, could not invoke");
      }
    }
  }
}