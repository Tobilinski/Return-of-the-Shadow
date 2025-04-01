using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ColliderEventTrigger : SerializedMonoBehaviour
{
    [Title("Other Collider Colliding List")] 
    [SerializeField] private HashSet<Collider2D> colliders;
    [Title("Unity Events")]
    public UnityEvent onCollisionEnter;
    public UnityEvent<PlayerController> onCollisionEnterPlayer;
    public UnityEvent<GameObject> onCollisionEnterBoomerang;
    public UnityEvent onCollisionExit;
    public UnityEvent<PlayerController> onCollisionExitPlayer;
    public void OnCollisionEnter2D(Collision2D other)
    {
        colliders.Add(other.gameObject.GetComponent<Collider2D>());
        
        onCollisionEnter?.Invoke();
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
        else if (other.gameObject.CompareTag("Boomerang"))
        {
            onCollisionEnterBoomerang?.Invoke(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            onCollisionEnterPlayer?.Invoke(other.gameObject.GetComponent<PlayerController>());
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        colliders.Remove(other.gameObject.GetComponent<Collider2D>());
        onCollisionExit?.Invoke();
        if(other.gameObject.CompareTag("Player"))
        {
            onCollisionExitPlayer?.Invoke(other.gameObject.GetComponent<PlayerController>());
        }
    }
}
