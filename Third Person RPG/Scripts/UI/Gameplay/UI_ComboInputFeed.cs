using UnityEngine;
using UnityEngine.UI;

public class UI_ComboInputFeed : MonoBehaviour
{
    public string comboInputfeed = "";
    ButtonInput[] comboInputs = new ButtonInput[3];
    UI_Events ui_Events;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        ui_Events = Core.instance.UIEvents;
        ui_Events.Combo_Changed.Subscribe(UpdateComboInputFeed);
    }

    void UpdateComboInputFeed()
    {
        GetComboInputs();
        Translate();
        Print();
    }

    void GetComboInputs()
    {
        comboInputs = ui_Events.Combo_Get();
    }

    void Translate()
    {
        //foreach (int i in comboInputs)
        //{
        //    comboInputfeed += GetTranslation(i);
        //}

        for (int i = 0; i < comboInputs.Length; i++)
        {
            if(i == 0)
            {
                comboInputfeed += " " + GetTranslation(comboInputs[i]);
            }
            else
            {
                if (comboInputs[i] != 0)
                {
                    comboInputfeed += " + " + GetTranslation(comboInputs[i]);
                }
            }

        }
    }

    string GetTranslation(ButtonInput button)
    {

        switch (button)
        {
            case (ButtonInput.X):
                return "X";
            case (ButtonInput.Y):
                return "Y";
            case (ButtonInput.B):
                return "B";
        }

        return "Empty";
    }

    void Print()
    {
        text.text = comboInputfeed;
        comboInputfeed = "";
    }
}
