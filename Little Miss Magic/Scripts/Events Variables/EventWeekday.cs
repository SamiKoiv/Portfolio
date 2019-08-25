using UnityEngine;

public class EventWeekday : ScriptableObject
{
    public delegate void ChangeEvent(Weekday value);
    public event ChangeEvent OnChange;

    Weekday m_value;

    public Weekday Value
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


