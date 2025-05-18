using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GlobalSceneManager : MonoBehaviour
{
    private bool isPaused = false;
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void HomeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
