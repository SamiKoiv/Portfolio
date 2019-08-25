using UnityEngine;

// For objects that are to be used as is, like light switches etc.
public interface IInteractable
{
    void Interact();
    void InteractLong();
    Transform GetTransform();
    Vector3 GetPopupOffset();
    string GetInteractionMessage();
}
