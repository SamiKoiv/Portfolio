using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    GameObject titleScreen;
    GameObject optionsScreen;

    GameObject currentScreen;

    void Start()
    {
        titleScreen = transform.Find("Title Screen").gameObject;
        optionsScreen = transform.Find("Options Screen").gameObject;


        currentScreen = titleScreen;
        titleScreen.SetActive(true);
    }

    public void TitleScreen()
    {
        titleScreen.SetActive(true);
        currentScreen.SetActive(false);
        currentScreen = titleScreen;
    }

    public void OptionsScreen()
    {
        optionsScreen.SetActive(true);
        currentScreen.SetActive(false);
        currentScreen = optionsScreen;
    }
}
