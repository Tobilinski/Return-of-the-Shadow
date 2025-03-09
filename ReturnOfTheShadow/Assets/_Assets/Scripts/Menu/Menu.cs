using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quitButton.onClick.AddListener(Application.Quit);
    }
}
