using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Source link: https://answers.unity.com/questions/1362482/how-to-set-ui-button-as-selected-on-mouse-over.html
    // This script is neccessary and needs to be attached to each in-game menu button so that the first button is not
    // highlighted when the mouse hovers over to another button
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");

        // If the mouse hovers over to another button, shift the color to the newly hovered over button
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the button that the cursor left from back to default color white
        this.GetComponent<Selectable>().OnPointerExit(null);
    }
}
