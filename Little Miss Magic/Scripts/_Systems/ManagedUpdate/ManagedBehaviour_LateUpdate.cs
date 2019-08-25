using UnityEngine;

public abstract class ManagedBehaviour_LateUpdate : MonoBehaviour
{
    public abstract void M_LateUpdate();

    protected void Subscribe_LateUpdate()
    {
            ManagedUpdate.Subscribe(this);
    }

    protected void Unsubscribe_LateUpdate()
    {
            ManagedUpdate.Unsubscribe(this);
    }
}
