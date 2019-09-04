using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void Add(int item);
    bool Reduce(int id);
    bool Contains(int id);
    bool Contains(int id, out int quantity);
}
