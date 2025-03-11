using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GameObject buttonSelected;
    private bool selected;
    private Animator animator;

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = EventSystem.current.currentSelectedGameObject;
        selected = true;
        animator = buttonSelected.GetComponent<Animator>();
        animator.Play("PlayButtonSelected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
        animator.Play("PlayButtonDeselected");
    }
}