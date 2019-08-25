using InventorySystem;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public enum ItemType
    {
        Other,
        Unique,
        Clothing,
        Consumable
    }

    [SerializeField] protected string itemName;
    public string ItemName
    {
        get { return itemName; }
    }

    [SerializeField] protected int price;
    public int Price
    {
        get { return price; }
    }

    [SerializeField, TextArea] protected string description;
    public string Description
    {
        get { return description; }
    }

    [SerializeField] protected GameObject worldPrefab;
    public GameObject WorldPrefab
    {
        get { return worldPrefab; }
    }

    [SerializeField] protected Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }

    public abstract ItemType GetItemType();

    // This indicates if the WorldPrefab is an empty container that needs to be loaded with Item?
    public abstract bool Stacks();

    public abstract bool CanUse();

    public abstract bool CanDiscard();

    public abstract void Use();

    public void Set(ItemBase itemBase)
    {
        itemName = itemBase.ItemName;
        price = itemBase.Price;
        description = itemBase.Description;
        worldPrefab = itemBase.WorldPrefab;
        icon = itemBase.Icon;
    }

    public bool MatchesWith(Item item)
    {
        if (item != null && this == item)
            return true;

        return false;
    }

    public bool MatchesWith(InventoryEntry entry)
    {
        if (entry != null && this == entry.Item)
            return true;

        return false;
    }
}
