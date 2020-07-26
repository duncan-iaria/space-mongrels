using System.Collections.Generic;
using UnityEngine;

namespace SNDL
{
  [CreateAssetMenu]
  public class StringEvent : ScriptableObject
  {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised. The String Variant
    /// </summary>
    private readonly List<StringEventListener> eventListeners =
        new List<StringEventListener>();


    public void raise(string tString)
    {
      for (int i = eventListeners.Count - 1; i >= 0; i--)
        eventListeners[i].onEventRaised(tString);
    }

    public void registerListener(StringEventListener listener)
    {
      if (!eventListeners.Contains(listener))
      {
        eventListeners.Add(listener);
      }
    }

    public void unregisterListener(StringEventListener listener)
    {
      if (eventListeners.Contains(listener))
      {
        eventListeners.Remove(listener);
      }
    }
  }
}