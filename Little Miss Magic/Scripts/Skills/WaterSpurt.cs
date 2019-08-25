using System.Collections.Generic;
using UnityEngine;

public class WaterSpurt : MonoBehaviour, ISpell
{
    public ParticleSystem m_ParticleSystem;
    public float m_EmissionRate;
    public float m_ParticleSpeed;

    void OnValidate()
    {
        var main = m_ParticleSystem.main;
        main.startSpeed = m_ParticleSpeed;
    }

    public void Cast()
    {
        var emission = m_ParticleSystem.emission;
        emission.rateOverTime = m_EmissionRate;
    }

    public void Uncast()
    {
        var emission = m_ParticleSystem.emission;
        emission.rateOverTime = 0;
    }
}
