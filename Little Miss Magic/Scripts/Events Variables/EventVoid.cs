using UnityEngine;

[CreateAssetMenu(menuName = "Events/Void Event")]
public class EventVoid : ScriptableObject
{
    public delegate void ChangeEvent();
    public event ChangeEvent OnChange;

    public void Trigger()
    {
        if (OnChange != null)
            OnChange();
    }
}
