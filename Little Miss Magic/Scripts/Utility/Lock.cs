using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour, ILock, ICombinable
{
    [SerializeField] string requiredKey;
    [SerializeField] string combinationMessage;
    [SerializeField] Vector3 popupOffset;
    [SerializeField] bool gizmos;

    [SerializeField] UnityEvent unlockEvent;
    IKey key;

    public void Combine(Rigidbody rb)
    {
         key = rb.GetComponent<IKey>();

        if (key != null)
        {
            if (key.GetKey().Equals(requiredKey))
            {
                unlockEvent.Invoke();
            }
        }
    }

    public string GetCombinationMessage()
    {
        return combinationMessage;
    }

    public Vector3 GetPopupOffset()
    {
        return popupOffset;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Unlock(IKey _key)
    {
        if (_key.Equals(requiredKey))
        {
            unlockEvent.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + popupOffset, 0.1f);
    }
}
