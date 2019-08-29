using System.Collections.Generic;
using UnityEngine;

public class Inventory : IHandItem, IContainsItem, ITakeItem
{
    List<Item> items = new List<Item>();

    public void HandItem(Item item)
    {
        items.Add(item);
    }

    public bool ContainsItem(Item item)
    {
        if (items.Contains(item))
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

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
                quantity++;
        }

        if (quantity > 0)
            return true;
        else
            return false;
    }

    public Item TakeItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            return item;
        }
        else
        {
            return null;
        }
    }
}
