using System;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    public UnityEvent onCollisionEnter;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != null)
        {
            onCollisionEnter.Invoke();
        }
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
