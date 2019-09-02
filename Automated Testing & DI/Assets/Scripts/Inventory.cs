using UnityEngine;
using System.Collections.Generic;

public class Inventory : IInventory
{
    List<Item> m_items = new List<Item>();

    public void Store(Item item)
    {
        m_items.Add(item);
    }

    public Item Withdraw(int id)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].GetID() == id)
            {
                Item result = m_items[i];
                m_items.RemoveAt(i);
                return result;
            }
        }

        throw new InventoryException("Trying to withdraw non-existent item from inventory. Please use Contains() to ensure item availability.");
    }

    public bool Contains(int id)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].GetID() == id)
            {
                return true;
            }
        }

        return false;
    }

    public bool Contains(int id, out int quantity)
    {
        quantity = 0;

        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].GetID() == id)
                quantity++;
        }

        if (quantity > 0)
            return true;
        else
            return false;
    }
}
