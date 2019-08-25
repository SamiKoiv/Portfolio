using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton_ApplicationExit : MenuButton
{
    override public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
