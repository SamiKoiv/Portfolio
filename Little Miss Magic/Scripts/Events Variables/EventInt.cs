using UnityEngine;

[CreateAssetMenu(menuName = "Events/Integer Event")]
public class EventInt : ScriptableObject
{
    public delegate void ChangeEvent(int value);
    public event ChangeEvent OnChange;

    int m_value;

    public int Value
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
