using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class Item_Consumable : Item
{
    public override bool CanUse()
    {
        return true;
    }

    public override ItemType GetItemType()
    {
        return ItemType.Consumable;
    }

    public override bool Stacks()
    {
        return true;
    }

    public override void Use()
    {
        Debug.Log("Player ate the apple.");
        PlayerInventory.Instance.Reduce(this, 1);
    }

    public override bool CanDiscard()
    {
        return true;
    }
}
