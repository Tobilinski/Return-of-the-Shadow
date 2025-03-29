using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class InteractableObject : MonoBehaviour,IInteractable
{
    private static readonly int _isTrigger = Animator.StringToHash("isTrigger");
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if(animator != null) animator.SetTrigger(_isTrigger);
    }
}
