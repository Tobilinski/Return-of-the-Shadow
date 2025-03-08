using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CustomUnityEvent : UnityEvent<object> { }
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private CustomUnityEvent response;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        gameEvent.OnRegisterListener(this);
    }

    void OnDisable()
    {
        gameEvent.OnDeRegisterListener(this);
    }

    public void OnEventRaised(object data)
    {
        response.Invoke(data);
    }
}
