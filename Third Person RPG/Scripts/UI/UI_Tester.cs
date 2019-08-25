using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tester : MonoBehaviour
{

    public UI_Events ui_Events;

    public float HP;
    public int Combo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Entering UI Test Values...");
            ui_Events.HP_Set(HP);
            ui_Events.HitCount_Set(Combo);
        }
    }

}
