using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour, IWaterable
{
    public Transform m_Model;
    public CapsuleCollider m_Collider;

    public void Water()
    {
        Debug.Log(gameObject.name + " was watered.");
        m_Model.localScale += Vector3.one * 0.1f;
        m_Collider.height = m_Model.localScale.y;
    }
}
