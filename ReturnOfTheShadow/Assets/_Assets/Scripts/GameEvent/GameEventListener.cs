using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CustomUnityEvent : UnityEvent<object> { }
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private CustomUnityEvent response;
    [SerializeField] private UnityEvent<string> responseString;
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
        if (data is int)
        {
            responseString.Invoke(data.ToString());
            Debug.Log($"string show {data}");
        }
        response.Invoke(data);
    }
}
