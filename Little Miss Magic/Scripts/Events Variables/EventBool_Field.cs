using UnityEngine;

public class EventBool_Field : MonoBehaviour
{
    [Header ("Event")]
    [SerializeField] EventBool m_EventBool;
    [SerializeField] bool playerOnly = false;

    [Header ("OnEnter")]
    [SerializeField] bool m_triggerOnEnter = false;
    [SerializeField] bool m_enterValue = false;

    [Header("OnExit")]
    [SerializeField] bool m_triggerOnExit = false;
    [SerializeField] bool m_exitValue = false;

    void OnTriggerEnter(Collider other)
    {
        if (playerOnly)
        {
            if (other.CompareTag("Player"))
            {
                if (m_triggerOnEnter)
                {
                    m_EventBool.Value = m_enterValue;
                }
            }
        }
        else
        {
            if (m_triggerOnEnter)
            {
                m_EventBool.Value = m_enterValue;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (playerOnly)
        {
            if (other.CompareTag("Player"))
            {
                if (m_triggerOnExit)
                {
                    m_EventBool.Value = m_exitValue;
                }
            }
        }
        else
        {
            if (m_triggerOnExit)
            {
                m_EventBool.Value = m_exitValue;
            }
        }
    }

}
