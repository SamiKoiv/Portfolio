using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSwitch : MonoBehaviour
{
    bool active;
    [SerializeField] EventBool placementActive;

    public void SwitchActive()
    {
        active = !active;

        switch (active)
        {
            case true:
                EventSystem.GameEvents.Placing_Start();
                placementActive.Value = true;
                break;
            case false:
                EventSystem.GameEvents.Placing_Stop();
                placementActive.Value = true;
                break;
        }
    }
}
