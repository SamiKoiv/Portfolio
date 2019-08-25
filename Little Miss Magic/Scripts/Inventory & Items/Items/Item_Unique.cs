using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Unique Item")]
public class Item_Unique : Item
{
    [SerializeField] bool canUse;

    public override bool CanUse()
    {
        return canUse;
    }

    public override ItemType GetItemType()
    {
        return ItemType.Unique;
    }

    public override bool Stacks()
    {
        return false;
    }

    public override void Use()
    {
        
    }

    public override bool CanDiscard()
    {
        return false;
    }
}
