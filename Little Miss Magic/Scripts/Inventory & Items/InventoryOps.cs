using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryOps
    {
        public static void MoveItem(Item item, int quantity, Inventory inventoryFrom, Inventory inventoryTo)
        {
            int amount = inventoryFrom.GetAmount(item);
            
            if (amount >= quantity)
            {
                inventoryTo.Add(item, quantity);
                inventoryFrom.Reduce(item, quantity);
            }
        }
    }
}