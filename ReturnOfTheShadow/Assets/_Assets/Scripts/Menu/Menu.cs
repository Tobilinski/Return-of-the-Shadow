using System;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private LeanWindow landingPageLeanWindow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        playButton.onClick.AddListener(CloseLandingPage);
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void CloseLandingPage()
    {
        landingPageLeanWindow.TurnOff();
    }
}
