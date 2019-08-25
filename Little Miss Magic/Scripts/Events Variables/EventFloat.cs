using UnityEngine;

[CreateAssetMenu(menuName = "Events/Float Event")]
public class EventFloat : ScriptableObject
{
    public delegate void ChangeEvent(float value);
    public event ChangeEvent OnChange;

    float m_value;

    public float Value
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
