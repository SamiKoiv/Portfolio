using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnableObject : MonoBehaviour, IBurnable, IWaterable
{
    [Header("Properties")]
    public bool Burnable;
    public bool Extinguishable;

    [Header("Events")]
    public UnityEvent BurnEvent;
    public UnityEvent ExtinguishEvent;

    public void Burn()
    {
        if (Burnable)
        {
            Debug.Log(transform.name + " burns with in bright color");
            BurnEvent.Invoke();
        }
    }

    public void Water()
    {
        if (Extinguishable)
        {
            Debug.Log(transform.name + " was extinguished and only small hints of smoke remain");
            ExtinguishEvent.Invoke();
        }
    }
}
