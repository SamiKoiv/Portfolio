using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerInventory : Inventory
{
    static PlayerInventory instance;
    public static PlayerInventory Instance
    {
        get { return instance; }
    }

    override protected void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Destroying PlayerInventory duplicates from the scene. Please check Scene for duplicates.");
            Destroy(gameObject);
        }

        InitializeEntries(Capacity);
    }
}
