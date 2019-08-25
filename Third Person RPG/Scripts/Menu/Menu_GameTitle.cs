using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_GameTitle : MonoBehaviour
{

    void Start()
    {
        Color_Library colorLibrary = DataLibrary.instance.colorLibrary;
        GetComponent<Text>().color = colorLibrary.menuButton;
        GetComponent<Outline>().effectColor = colorLibrary.menuButtonOutline;
    }

}
