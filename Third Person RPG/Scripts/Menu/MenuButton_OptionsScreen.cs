using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton_OptionsScreen : MenuButton
{

    MainMenu mainMenu;

    override public void Start()
    {
        Color_Library colorLibrary = DataLibrary.instance.colorLibrary;
        mainColor = colorLibrary.menuButton;
        mainHighlight = colorLibrary.menuButtonHighlight;
        outlineColor = colorLibrary.menuButtonOutline;
        outlineHighlight = colorLibrary.menuButtonOutlineHighlight;
        MainColor();
        GetMainMenu();
    }

    override public void OnPointerClick(PointerEventData eventData)
    {
        mainMenu.OptionsScreen();
    }

    void GetMainMenu()
    {
        Transform i = transform.parent;
        while(mainMenu == null)
        {
            if (i.name == "Main Menu")
            {
                mainMenu = i.GetComponent<MainMenu>();
            }

            i = i.parent;

        }
    }
}
