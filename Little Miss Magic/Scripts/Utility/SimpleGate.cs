using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGate : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;

        EventSystem.InteractionEvents.CombinableDestroyed(GetComponent<ICombinable>());
    }
}
