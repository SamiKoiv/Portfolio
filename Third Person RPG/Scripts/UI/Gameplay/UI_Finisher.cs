using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Finisher : MonoBehaviour
{

    UI_Events ui_Events;
    Finisher finisher;
    string finisherName;
    string printOut;

    Text text;

    void Start()
    {
        ui_Events = Core.instance.UIEvents;
        ui_Events.Finisher_Changed.Subscribe(OnFinisherChanged);
        text = GetComponent<Text>();
    }

    void OnFinisherChanged()
    {
        finisher = ui_Events.Finisher_Get();
        Translate();
        PrintFinisher();
    }

    void Translate()
    {
        switch (finisher)
        {
            case Finisher.XXX:
                finisherName = "XXX";
                break;
            case Finisher.XXY:
                finisherName = "XXY";
                break;
            case Finisher.XYX:
                finisherName = "XYX";
                break;
            case Finisher.XYY:
                finisherName = "XYY";
                break;
            case Finisher.YXX:
                finisherName = "YXX";
                break;
            case Finisher.YXY:
                finisherName = "YXY";
                break;
            case Finisher.YYX:
                finisherName = "YYX";
                break;
            case Finisher.YYY:
                finisherName = "YYY";
                break;
        }

        if (finisher == Finisher.empty)
        {
            printOut = "";
        }
        else
        {
            printOut = "Finisher no: " + finisherName;
        }
    }

    void PrintFinisher()
    {
        text.text = printOut;
    }

}
