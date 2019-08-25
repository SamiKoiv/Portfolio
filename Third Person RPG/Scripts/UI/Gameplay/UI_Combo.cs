using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Combo : MonoBehaviour
{
    public UI_Events ui_Events;

    int ComboCount;
    Text Combo_Text;
    public string Description = "Combo: ";
    public int DefaultFontSize = 14;

    void Awake()
    {
        Combo_Text = GetComponent<Text>();
    }

    void OnEnable()
    {
        ui_Events.HitCount_Changed.Subscribe(Combo_Changed);
    }

    void OnDisable()
    {
        ui_Events.HitCount_Changed.Unsubscribe(Combo_Changed);
    }

    void Combo_Changed()
    {
        Get_Combo();
        Update_Text();
    }

    void Get_Combo()
    {
        ComboCount = ui_Events.HitCount_Get();
    }

    void Update_Text()
    {
        if (ComboCount >= 30)
        {
            Combo_Text.text = "" + ComboCount;
        }
        else
        {
            Combo_Text.text = Description + ComboCount;
        }
        
        Combo_Text.fontSize = DefaultFontSize + ComboCount;

        if (ComboCount == 0)
        {
            Combo_Text.enabled = false;
        }
        else if (ComboCount != 0 && Combo_Text.enabled == false)
        {
            Combo_Text.enabled = true;
        }
    }

}
