using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    internal Color mainColor;
    internal Color mainHighlight;
    internal Color outlineColor;
    internal Color outlineHighlight;

    internal Text text;
    internal Outline outline;

    public UnityEvent clickEvent;

    void Awake()
    {
        text = GetComponent<Text>();
        outline = GetComponent<Outline>();
    }

    virtual public void Start()
    {
        Color_Library colorLibrary = DataLibrary.instance.colorLibrary;
        mainColor = colorLibrary.menuButton;
        mainHighlight = colorLibrary.menuButtonHighlight;
        outlineColor = colorLibrary.menuButtonOutline;
        outlineHighlight = colorLibrary.menuButtonOutlineHighlight;
        MainColor();
    }

    internal void MainColor()
    {
        text.color = mainColor;
        outline.effectColor = outlineColor;
    }

    void HighlightColor()
    {
        text.color = mainHighlight;
        outline.effectColor = outlineHighlight;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightColor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MainColor();
    }

    virtual public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke();
    }

}
