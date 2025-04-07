using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class MovingPlatformController : MonoBehaviour
{
    private Animator animator;
    [FormerlySerializedAs("hasStop")] [SerializeField,ToggleLeft] private bool hasSecondAnimation;

    [SerializeField] private string animationName; 
    [ShowIf("hasSecondAnimation"), SerializeField] private string animation2;
    private bool isTriggered;
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
    
}
