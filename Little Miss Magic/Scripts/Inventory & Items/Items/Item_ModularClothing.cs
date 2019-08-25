using UMA;
using UMA.CharacterSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Modular Clothing")]
public class Item_ModularClothing : Item
{
    public Item_ModularClothing(string itemName, int price, string description, GameObject worldPrefab, Sprite icon)
    {
        base.itemName = itemName;
        base.price = price;
        base.description = description;
        base.worldPrefab = worldPrefab;
        base.icon = icon;
    }

    public UMATextRecipe[] Recipes;
  

    public void ApplyClothing(DynamicCharacterAvatar avatar)
    {
        foreach (UMATextRecipe r in Recipes)
        {
            avatar.SetSlot(r);
        }
    }

    public void ClearClothing(DynamicCharacterAvatar avatar)
    {
        foreach (UMATextRecipe r in Recipes)
        {
            avatar.ClearSlot(r.wardrobeSlot);
        }
    }

    public bool OverlapsOn(string slot)
    {
        foreach (UMATextRecipe r in Recipes)
        {
            if (r.wardrobeSlot.Equals("slot"))
                return true;
        }

        return false;
    }

    public override bool Stacks()
    {
        return false;
    }

    new public GameObject WorldPrefab
    {
        get
        {
            worldPrefab.GetComponent<ItemProxy>().Set(this, 1);
            return worldPrefab;
        }
    }

    public override bool CanUse()
    {
        return true;
    }

    public override bool CanDiscard()
    {
        return true;
    }

    public override void Use()
    {
        DynamicCharacterAvatar avatar = EventSystem.Objects.Player.GetComponentInChildren<ModularClothingProxy>().CharacterAvatar;
        ApplyClothing(avatar);
        avatar.BuildCharacter();
        Debug.Log("Clothing Applied.");
    }

    public override ItemType GetItemType()
    {
        return ItemType.Clothing;
    }
}
