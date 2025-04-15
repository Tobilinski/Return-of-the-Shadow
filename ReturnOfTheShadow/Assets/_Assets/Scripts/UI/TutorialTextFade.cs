using UnityEngine;

public class TutorialTextFade : MonoBehaviour
{
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }
    }
    public void Play()
    {
        animator.Play("Fade");
    }
}
