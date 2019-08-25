using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour
{
    public UnityEvent damageEvent;
    public UnityEvent healEvent;
    float _damage;
    float _heal;

    public float Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
            damageEvent.Invoke();
        }
    }

    public float Heal
    {
        get
        {
            return _heal;
        }

        set
        {
            _heal = value;
            healEvent.Invoke();
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
