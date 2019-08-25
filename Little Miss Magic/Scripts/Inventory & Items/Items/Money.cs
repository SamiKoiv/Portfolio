using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Money")]
public class Money : Item
{
    public override bool Stacks()
    {
        return true;
    }

    public override bool CanUse()
    {
        return false;
    }

    public override void Use()
    {
        
    }

    public override ItemType GetItemType()
    {
        return ItemType.Other;
    }

    public override bool CanDiscard()
    {
        return false;
    }
}
