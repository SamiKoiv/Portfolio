using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using InventorySystem;

public class UI_InventoryDescriptionScreen : MonoBehaviour
{
    public GameObject ElementHolder;
    public Image ItemIcon;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemPrice;
    public TextMeshProUGUI ItemQuantity;

    public Item DebugItem;

    InventoryEntry entry;

    private void OnEnable()
    {
        UISystem.OnDescription_Set += SetItem;
    }

    private void OnDisable()
    {
        UISystem.OnDescription_Set -= SetItem;
    }

    public void SetItem(InventoryEntry entry)
    {
        if (entry.Item == null)
            return;

        this.entry = entry;

        ElementHolder.SetActive(true);

        ItemName.text = entry.ItemName;
        ItemIcon.sprite = entry.Icon;
        ItemDescription.text = entry.Description;
        ItemPrice.text = string.Empty + entry.Price;
        ItemQuantity.text = string.Empty + entry.Quantity;
    }

    public void UseItem()
    {
        Debug.Log("Using " + entry.ItemName);
        entry.Item.Use();
        UISystem.Instance.Description_Close();
    }

    public void PlaceItem()
    {
        Debug.Log("Placing " + entry.ItemName);
        EventSystem.GameEvents.Placing_Start();

        EventSystem.GameEvents.Placing_PlaceEntry(entry, 1);

        UISystem.Instance.Description_Close();
        UISystem.Instance.Inventory_Close();
    }

    public void Cancel()
    {
        UISystem.Instance.Description_Close();
    }
}
