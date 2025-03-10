using System;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonSelect : MonoBehaviour,ISelectHandler, IDeselectHandler
{
    private GameObject buttonSelected;
    private Vector3 targetRotation = new Vector3(0,0,5);    
    private Vector3 targetScale = new Vector3(1.1f,1.1f,1.1f);
    private Vector3 resetScale = new Vector3(1,1,1);
    private Vector3 resetRotation= new Vector3(0,0,0);
    private float timeToTargetRotation = 0.2f;
    private bool selected;

    private void Update()
    {
        if (selected)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, timeToTargetRotation);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, timeToTargetRotation);
        }
        else
        {
            if (transform.eulerAngles != resetRotation)
            {
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, resetRotation, timeToTargetRotation);
                transform.localScale = Vector3.Lerp(transform.localScale, resetScale, timeToTargetRotation);
                print("Resetting");
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = EventSystem.current.currentSelectedGameObject;
        selected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
    }
}
