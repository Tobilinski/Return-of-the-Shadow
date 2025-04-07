using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class MovingPlatformController : MonoBehaviour
{
    private Animator animator;
    [SerializeField,ToggleLeft] private bool hasSecondAnimation;
    [SerializeField,ToggleLeft] private bool hasStopAnimation;

    [SerializeField] private string animationName; 
    [ShowIf("hasSecondAnimation"), SerializeField] private string animation2;
    private bool isTriggered;

    private bool isStopped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    //Called in editor
    public void PlayAnimation()
    {
        isTriggered = !isTriggered;
        if (isTriggered)
        {
            animator.Play(animationName);
        }
        else if(hasSecondAnimation && !isTriggered)
        {
            animator.Play(animation2);
        }
    }
    //Called in editor
    public void PlayStopAnimation()
    {
        isStopped = !isStopped;
        if (isStopped)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }
}
