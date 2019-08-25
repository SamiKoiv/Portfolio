using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(SphereCollider))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] string interactionMessage;
    [SerializeField] bool destroyedOnInteract;
    [SerializeField] bool destroyedOnInteractLong;
    [SerializeField] Vector3 popupOffset;
    [SerializeField] bool gizmos;
    [SerializeField] UnityEvent interactionEvent;
    [SerializeField] UnityEvent interactionLongEvent;

    public void Interact()
    {
        Debug.Log("Player interacted with: " + transform.name);
        interactionEvent.Invoke();

        if (destroyedOnInteract)
            ObjectDestroy();
    }

    public void InteractLong()
    {
        Debug.Log("Player interacted long with: " + transform.name);
        interactionLongEvent.Invoke();

        if (destroyedOnInteractLong)
            ObjectDestroy();
    }

    public Transform GetTransform()
    {
        return transform;
    }


    public Vector3 GetPopupOffset()
    {
        return popupOffset;
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    void ObjectDestroy()
    {
        EventSystem.InteractionEvents.InteractableDestroyed(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + popupOffset, 0.1f);
    }
}
