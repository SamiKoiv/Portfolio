using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_PunchingBag : MonoBehaviour
{

    public GameObject hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        GameObject.Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
