using UnityEngine;

public abstract class ManagedBehaviour_Update : MonoBehaviour
{
    public abstract void M_Update();

    protected void Subscribe_Update()
    {
            ManagedUpdate.Subscribe(this);
    }

    protected void Unsubscribe_Update()
    {
            ManagedUpdate.Unsubscribe(this);
    }
}
