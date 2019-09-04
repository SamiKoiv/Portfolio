using System.Collections.Generic;

public class InventoryByID : IInventory
{
    List<int> m_itemIDs = new List<int>();

    public void Add(int itemID)
    {
        m_itemIDs.Add(itemID);
    }

    public bool Reduce(int id)
    {
        return m_itemIDs.Remove(id);
    }

    public bool Contains(int id)
    {
        for (int i = 0; i < m_itemIDs.Count; i++)
        {
            if (m_itemIDs[i] == id)
            {
                return true;
            }
        }

        return false;
    }

    public bool Contains(int id, out int quantity)
    {
        quantity = 0;

        for (int i = 0; i < m_itemIDs.Count; i++)
        {
            if (m_itemIDs[i] == id)
                quantity++;
        }

        if (quantity > 0)
            return true;
        else
            return false;
    }
}
