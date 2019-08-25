using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticles : MonoBehaviour
{
    public ParticleSystem m_ParticleSystem;

    //List<UnityEngine.ParticleCollisionEvent> m_collisionEvents = new List<UnityEngine.ParticleCollisionEvent>();

    IWaterable waterable;

    void OnParticleCollision(GameObject other)
    {
        //m_ParticleSystem.GetCollisionEvents(other, m_collisionEvents);

        waterable = other.GetComponent<IWaterable>();

        if (waterable != null)
        {
            waterable.Water();
        }
    }
}
