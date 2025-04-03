using UnityEngine;
using TMPro;

public class UITriggerTutorial : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player!");
        }
    }

}

