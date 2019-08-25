using UnityEngine;

[CreateAssetMenu(menuName = "Events/Boolean Event")]
public class EventBool : ScriptableObject
{
    public delegate void ChangeEvent(bool value);
    public event ChangeEvent OnChange;

    bool m_value;

    public bool Value
    {
        get
        {
            return m_value;
        }

        set
        {
            m_value = value;

            if (OnChange != null)
                OnChange(m_value);
        }
    }
}
