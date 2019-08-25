using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public struct ItemBase
    {
        public ItemBase(string itemName, int price, string description, GameObject worldPrefab, Sprite icon)
        {
            ItemName = itemName;
            Price = price;
            Description = description;
            WorldPrefab = worldPrefab;
            Icon = icon;
        }

        public string ItemName;
        public int Price;
        public string Description;
        public GameObject WorldPrefab;
        public Sprite Icon;
    }
}