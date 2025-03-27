using System;
using UnityEngine;

public class ColliderEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent onCollisionEnter;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            onCollisionEnter?.Raise(other.gameObject);
            interactable.Interact();
        }
    }
}
