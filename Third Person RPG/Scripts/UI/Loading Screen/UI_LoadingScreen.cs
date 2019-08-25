using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingScreen : MonoBehaviour
{
    [Header("Background Fade")]
    public Image background;
    bool backgroundFound = false;
    public bool fadeOut;
    public float fadeSpeed;
    float fade;
    public Color unfadedColor;
    public Color fadedColor;

    [Header("Loading Text")]
    public Text loadingText;
    bool loadingTextFound = false;
    public string loadText = "Loading";
    float t = 0;

    [Header("Rotator")]
    public RectTransform rotator;
    bool rotatorFound = false;
    public float rotatorSpeed;

    public bool FadeOut
    {
        get
        {
            return fadeOut;
        }
        set
        {
            fadeOut = value;
        }
    }
    public bool FadeComplete
    {
        get
        {
            if (fade == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //------------------------------------------------------------

    public void ManagedInit()
    {

        if (background != null)
        {
            backgroundFound = true;
            background.gameObject.SetActive(true);
        }

        if (loadingText != null)
        {
            loadingTextFound = true;
            loadingText.gameObject.SetActive(true);
        }

        if (rotator != null)
        {
            rotatorFound = true;
            rotator.gameObject.SetActive(true);
        }

        fade = 1;
    }

    public void ManagedUpdate()
    {
        FadeBackground();
        FadeElements();
        LoadingText();
        Rotator();
    }

    void FadeBackground()
    {
        if (backgroundFound)
        {
            if (fadeOut)
            {
                fade = Mathf.Min(1, fade + Time.deltaTime * fadeSpeed);
                background.color = Color.Lerp(unfadedColor, fadedColor, fade);
            }
            else
            {
                fade = Mathf.Max(0, fade - Time.deltaTime * fadeSpeed);
                background.color = Color.Lerp(unfadedColor, fadedColor, fade);
            }
        }
    }

    void FadeElements()
    {
        if (fade == 1)
        {
            loadingText.enabled = true;
            rotator.gameObject.SetActive(true);
            t += Time.deltaTime;
        }
        else
        {
            loadingText.enabled = false;
            rotator.gameObject.SetActive(false);
            t = 0;
        }
    }

    void LoadingText()
    {
        if (loadingTextFound)
        {
            if (t < 1)
            {
                loadingText.text = loadText + "";
            }
            else if (t < 2)
            {
                loadingText.text = loadText + ".";
            }
            else if (t < 3)
            {
                loadingText.text = loadText + "..";
            }
            else if (t < 4)
            {
                loadingText.text = loadText + "...";
            }
            else
            {
                t = 0;
            }
        }
    }

    void Rotator()
    {
        if (rotatorFound)
        {
            rotator.Rotate(Vector3.forward * Mathf.Sin(Time.time) * rotatorSpeed * Time.deltaTime);
        }
    }

    
}
