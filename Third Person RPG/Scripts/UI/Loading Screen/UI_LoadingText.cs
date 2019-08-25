using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingText : MonoBehaviour
{

    string loadText = "Loading";
    float t = 0;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (t < 1)
        {
            text.text = loadText;
        }
        else if (t < 2)
        {
            text.text = loadText + ".";
        }
        else if (t < 3)
        {
            text.text = loadText + "..";
        }
        else if (t < 4)
        {
            text.text = loadText + "...";
        }
        else
        {
            t = 0;
        }

        t += Time.deltaTime;
    }
}
