using TMPro;
using UnityEngine;

public class InteractionPopup : ManagedBehaviour_LateUpdate
{
    new Transform transform;
    public Transform m_SignHolder;
    public TextMeshProUGUI m_Message;
    public float m_SignHeight;

    Transform m_target;
    Vector3 m_offset;

    void Awake()
    {
        transform = gameObject.transform;

        transform.localPosition = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        m_SignHolder.localPosition = Vector3.up * m_SignHeight;
    }

    void OnEnable()
    {
        Subscribe_LateUpdate();
        EventSystem.InteractionEvents.OnSetInteractionPopup += SetPopup;
    }

    void OnDisable()
    {
        Unsubscribe_LateUpdate();
        EventSystem.InteractionEvents.OnSetInteractionPopup -= SetPopup;
    }

    void OnValidate()
    {
        m_SignHolder.localPosition = Vector3.up * m_SignHeight;
    }

    public override void M_LateUpdate()
    {
        if (m_target == null)
            return;

        transform.position = m_target.position + m_offset;

        transform.eulerAngles = Vector3.zero;
        m_SignHolder.LookAt(Core.Instance.m_MainCamera);
    }

    void SetPopup(bool isActive, Transform target, Vector3 offset, string message)
    {
        m_SignHolder.gameObject.SetActive(isActive);
        m_target = target;
        m_offset = offset;
        m_Message.text = "" + message;
    }
}
