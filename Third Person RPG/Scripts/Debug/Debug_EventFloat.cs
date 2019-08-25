using UnityEngine;

public class Debug_EventFloat : MonoBehaviour
{
    public Event_Float eventFloat;
    public float setValue;

    public void SetValue()
    {
        eventFloat.Value = setValue;
    }

}
