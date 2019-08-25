using UnityEngine;
using UnityEngine.Events;
using InventorySystem;

public class ItemProxy : MonoBehaviour, IInteractable
{
    [SerializeField] Item item;
    public Item ContainedItem
    {
        get
        {
            return item;
        }
    }

    [SerializeField] int quantity;
    public int ContainedQuantity
    {
        get
        {
            return quantity;
        }
    }

    [SerializeField] Vector3 popupOffset;
    [SerializeField] bool gizmos;

    public void Pick()
    {
        Debug.Log(item.ItemName + " added to inventory.");
        PlayerInventory.Instance.Add(item, quantity);

        EventSystem.InteractionEvents.InteractableDestroyed(this);
        Destroy(gameObject);
    }

    public void Set(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Interact()
    {

    }

    public void InteractLong()
    {
        Debug.Log("Picking up " + item.ItemName);
        Pick();
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
        return "Pick";
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + popupOffset, 0.1f);
    }
}
