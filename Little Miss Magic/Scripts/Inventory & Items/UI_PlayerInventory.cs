using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerInventory : MonoBehaviour
{
    [SerializeField] UI_Inventory inventory;
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] EventBool inventoryOpen;

    private void OnEnable()
    {
        inventoryOpen.OnChange += SetInventoryOpen;
    }

    private void OnDisable()
    {
        inventoryOpen.OnChange -= SetInventoryOpen;
    }

    private void Start()
    {
        inventory.BindToInventory(PlayerInventory.Instance);
    }

    void SetInventoryOpen(bool open)
    {
        if (open)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    void OpenInventory()
    {
        //inventoryWindow.SetActive(true);
        inventory.SetItems();
    }

    void CloseInventory()
    {
        //inventoryWindow.SetActive(false);
    }
}
