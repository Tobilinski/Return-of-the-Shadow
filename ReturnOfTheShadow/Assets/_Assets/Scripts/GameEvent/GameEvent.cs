using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(object data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }
    public void OnRegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    public void OnDeRegisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
