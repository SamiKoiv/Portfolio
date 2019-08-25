using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public bool altPhysics;
    public float m_MovementSpeed;
    public float m_Lifetime;

    float m_time = 0;

    IBurnable m_burnableObject;

    void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * m_MovementSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        m_burnableObject = other.GetComponent<IBurnable>();

        if (m_burnableObject != null)
        {
            m_burnableObject.Burn();
            m_burnableObject = null;
        }

        GameObject.Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        m_burnableObject = collision.transform.GetComponent<IBurnable>();

        if (m_burnableObject != null)
        {
            m_burnableObject.Burn();
            m_burnableObject = null;
        }

    }

    void Update()
    {
        if (m_time > m_Lifetime)
        {
            GameObject.Destroy(gameObject);
        }

        m_time += Time.deltaTime;
    }

    void OnValidate()
    {
        GetComponent<Rigidbody>().useGravity = altPhysics;
        GetComponent<Collider>().isTrigger = !altPhysics;
    }
}
