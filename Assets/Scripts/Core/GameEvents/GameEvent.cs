using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            return;

        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            return;

        listeners.Remove(listener);
    }

    public void InvokeEvent(GameEventData data = null)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventInvoked(data);
        }
    }
}

[System.Serializable]
public class GameEventResponse : UnityEvent<GameEventData> { }
