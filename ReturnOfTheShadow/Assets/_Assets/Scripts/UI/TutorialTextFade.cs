using UnityEngine;

public class TutorialTextFade : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Fade");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("Fade");
        }
    }
}
