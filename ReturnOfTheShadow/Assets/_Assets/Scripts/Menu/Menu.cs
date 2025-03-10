using System;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    private LeanHover playHover;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button levelBackButton;
    [SerializeField] private LeanWindow landingPageLeanWindow;
    [SerializeField] private LeanWindow levelSelectLeanWindow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        playButton.onClick.AddListener(CloseLandingPage);
        playHover = playButton.gameObject.AddComponent<LeanHover>();
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
