using System.Collections.Generic;
using UnityEngine;

public class Inventory : IHandItem, IContainsItem, ITakeItem
{
    List<Item> m_items = new List<Item>();

    public void HandItem(Item item)
    {
        m_items.Add(item);
    }

    public bool ContainsItem(Item item)
    {
        if (m_items.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ContainsItem(Item item, out int quantity)
    {
        quantity = 0;

        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i] == item)
                quantity++;
        }

        if (quantity > 0)
            return true;
        else
            return false;
    }

    public Item TakeItem(Item item)
    {
        if (m_items.Contains(item))
        {
            m_items.Remove(item);
            return item;
        }
        else
        {
            return null;
        }
    }
}
