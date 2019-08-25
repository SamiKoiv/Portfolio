using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCannon : MonoBehaviour, ICombinable
{
    [SerializeField] Transform m_barrel;
    [SerializeField] float m_shootForce;
    [SerializeField] Vector3 m_addSpin;
    [SerializeField] string m_combinationMessage;
    [SerializeField] Vector3 m_popupOffset;

    public void Combine(Rigidbody rb)
    {
        rb.position = m_barrel.position;
        rb.isKinematic = false;
        rb.GetComponent<Collider>().enabled = true;
        rb.AddForce(m_barrel.forward * m_shootForce, ForceMode.Impulse);
        rb.AddTorque(m_addSpin, ForceMode.VelocityChange);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public string GetCombinationMessage()
    {
        return m_combinationMessage;
    }

    public Vector3 GetPopupOffset()
    {
        return m_popupOffset;
    }
}
