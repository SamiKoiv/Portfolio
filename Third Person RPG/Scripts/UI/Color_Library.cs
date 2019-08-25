using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Library : ScriptableObject
{
    public Color menuButton;
    public Color menuButtonHighlight;
    public Color menuButtonOutline;
    public Color menuButtonOutlineHighlight;

    public void Ping()
    {
        Debug.Log(name + "PING!");
    }

}
