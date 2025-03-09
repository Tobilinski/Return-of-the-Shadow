using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelBackButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private LeanWindow landingPageLeanWindow;
    [SerializeField] private LeanWindow levelSelectLeanWindow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(CloseLandingPage);
        levelBackButton.onClick.AddListener(CloseLevelSelect);
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void CloseLandingPage()
    {
        landingPageLeanWindow.TurnOff();
        levelSelectLeanWindow.TurnOn();
    }

    private void CloseLevelSelect()
    {
        levelSelectLeanWindow.TurnOff();
        landingPageLeanWindow.TurnOn();
    }
}
