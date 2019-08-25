using System.Collections.Generic;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public class ModularClothingProxy : MonoBehaviour
{
    public DynamicCharacterAvatar CharacterAvatar;
    [ContextMenuItem("Set Slot", "SetClothing"), ContextMenuItem("Clear Slot", "ClearClothing")] public List<Item_ModularClothing> Clothing;

    Item_ModularClothing overlap;
    bool ready;

    bool IsCharacterReady
    {
        get
        {
            if (ready == true)
                return true;
            else
            {
                if (CharacterAvatar.isActiveAndEnabled)
                {
                    ready = true;
                    return true;
                }
                else
                {
                    Debug.Log("Character is not ready yet.");
                    return false;
                }
            }
        }
    }

    public void AddClothing(Item_ModularClothing clothing)
    {
        if (!IsCharacterReady)
            return;

        if (!CharacterAvatar.isActiveAndEnabled)


            if (IsOverlapping(clothing, out overlap))
            {
                RemoveClothing(overlap);
            }

        clothing.ApplyClothing(CharacterAvatar);
        Clothing.Add(clothing);

        BuildCharacter();
    }

    public void AddClothing(Item_ModularClothing clothing, bool buildCharacter)
    {
        if (!IsCharacterReady)
            return;

        if (IsOverlapping(clothing, out overlap))
        {
            RemoveClothing(overlap);
        }

        clothing.ApplyClothing(CharacterAvatar);
        Clothing.Add(clothing);

        if (buildCharacter)
            BuildCharacter();
    }

    public void RemoveClothing(Item_ModularClothing clothing)
    {
        if (!IsCharacterReady)
            return;

        clothing.ClearClothing(CharacterAvatar);
        Clothing.Remove(clothing);

        BuildCharacter();
    }

    public void RemoveClothing(Item_ModularClothing clothing, bool buildCharacter)
    {
        if (!IsCharacterReady)
            return;

        clothing.ClearClothing(CharacterAvatar);
        Clothing.Remove(clothing);

        if (buildCharacter)
            BuildCharacter();
    }

    public void SetClothing()
    {
        if (!IsCharacterReady)
            return;

        foreach (Item_ModularClothing c in Clothing)
        {
            c.ApplyClothing(CharacterAvatar);
        }

        BuildCharacter();
    }

    public void ClearClothing()
    {
        if (!IsCharacterReady)
            return;

        foreach (Item_ModularClothing c in Clothing)
        {
            c.ClearClothing(CharacterAvatar);
        }

        BuildCharacter();
    }

    public void BuildCharacter()
    {
        CharacterAvatar.BuildCharacter();
    }

    bool IsOverlapping(Item_ModularClothing clothing, out Item_ModularClothing overlap)
    {
        foreach (UMATextRecipe r in clothing.Recipes)
        {
            foreach (Item_ModularClothing c in Clothing)
            {
                if (c.OverlapsOn(r.wardrobeSlot))
                {
                    overlap = c;
                    return true;
                }
            }
        }

        overlap = null;
        return false;
    }
}
