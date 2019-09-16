using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void Deposit(int id);
    void Deposit(int id, int quantity);

    bool Withdraw(int id);
    bool Withdraw(int id, int quantity);

    bool Contains(int id);
    bool Contains(int id, out int quantity);
}
