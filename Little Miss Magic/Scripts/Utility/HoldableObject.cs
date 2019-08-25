using UnityEngine;

public class HoldableObject : MonoBehaviour, IHoldable
{
    Rigidbody rb;
    new Collider collider;

    bool hasRigidbody;
    bool hasCollider;

    bool isKinematic;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        if (rb != null)
        {
            hasRigidbody = true;
            isKinematic = rb.isKinematic;
        }

        if (collider != null)
            hasCollider = true;
    }

    public void TakeHold()
    {
        EventSystem.InteractionEvents.TakenHold(this, rb);

        if (hasRigidbody)
            rb.isKinematic = true;

        if (hasCollider)
            collider.enabled = false;
    }

    public void Drop()
    {
        if (hasRigidbody)
            rb.isKinematic = isKinematic;

        if (hasCollider)
            collider.enabled = true;
    }

    public void Throw(Vector3 throwForce)
    {
        if (hasRigidbody)
        {
            rb.isKinematic = isKinematic;
            rb.AddForce(throwForce, ForceMode.Impulse);
        }

        if (hasCollider)
            collider.enabled = true;

    }
}
