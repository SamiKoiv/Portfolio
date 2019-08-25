using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement_PickObject_Button : MonoBehaviour
{
    public GameObject Object;

    public void Pick()
    {
        EventSystem.GameEvents.Placing_PlaceGO(Object);
    }
}
