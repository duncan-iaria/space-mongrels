using System.Collections.Generic;
using UnityEngine;

namespace SNDL
{
  [CreateAssetMenu]
  public class GameEvent : ScriptableObject
  {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();


    public void raise()
    {
      for (int i = eventListeners.Count - 1; i >= 0; i--)
        eventListeners[i].onEventRaised();
    }

    public void registerListener(GameEventListener listener)
    {
      if (!eventListeners.Contains(listener))
      {
        eventListeners.Add(listener);
      }
    }

    public void unregisterListener(GameEventListener listener)
    {
      if (eventListeners.Contains(listener))
      {
        eventListeners.Remove(listener);
      }
    }
  }
}