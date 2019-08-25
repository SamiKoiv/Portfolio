using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton_StartGame : MenuButton
{
    override public void OnPointerClick(PointerEventData eventData)
    {
        Core.instance.SceneManager.LoadScene(1);
    }
}
