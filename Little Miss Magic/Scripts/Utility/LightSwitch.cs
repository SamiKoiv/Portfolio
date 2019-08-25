using UnityEngine;
using System.Collections.Generic;

public class LightSwitch : MonoBehaviour
{
    public EventBool m_EventBool;
    public List<Light> Lights;

    public bool m_switchOnInside;

    private void OnEnable()
    {
        m_EventBool.OnChange += Switch;
    }

    private void OnDisable()
    {
        m_EventBool.OnChange -= Switch;
    }

    private void Start()
    {
        Switch(m_EventBool.Value);
    }

    void Switch(bool state)
    {
        if (m_switchOnInside)
        {
            foreach(Light l in Lights)
            {
                l.enabled = state;
            }
        }
        else
        {
            foreach (Light l in Lights)
            {
                l.enabled = !state;
            }
        }
    }
}
