using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void Store(Item item);
    Item Withdraw(int id);
    bool Contains(int id);
    bool Contains(int id, out int quantity);
}
