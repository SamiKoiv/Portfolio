using System.Collections.Generic;

public class InventoryByID : IInventory
{
    List<int> m_itemIDs = new List<int>();

    public void Deposit(int itemID)
    {
        m_itemIDs.Add(itemID);
    }

    public void Deposit(int item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
            Deposit(item);
    }

    public bool Withdraw(int id)
    {
        return m_itemIDs.Remove(id);
    }

    public bool Withdraw(int id, int quantity)
    {
        bool result = false;

        for (int i = 0; i < quantity; i++)
            result = Withdraw(id);

        return result;
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
