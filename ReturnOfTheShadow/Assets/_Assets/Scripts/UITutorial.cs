using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UITutorial : MonoBehaviour
{
    public string animationName = "Fade"; // Set the name of your animation in the Animator
    private Animator animator;

    public TMP_Text textToDisplay;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();

        if (textToDisplay != null)
        {
            textToDisplay.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the desired key is pressed (for example, the "Space" key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Play the animation using its name
            animator.Play(animationName);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Play the animation using its name
            animator.Play(animationName);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Play the animation using its name
            animator.Play(animationName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player or specific object entered the trigger zone
        if (other.CompareTag("UITrigger"))
        {
            // Make the text visible when the player enters the trigger zone
            if (textToDisplay != null)
            {
                textToDisplay.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UITrigger"))
        {
            if (textToDisplay != null)
            {
                textToDisplay.gameObject.SetActive(false);
            }
        }
    }
}


