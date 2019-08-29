using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IGetName, IEquipWith, IGetEquipment
{
    protected string _name;
    Weapon _weapon;
    Armor _armor;

    public virtual string GetName()
    {
        return _name;
    }

    public void EquipWith(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void EquipWith(Armor armor)
    {
        _armor = armor;
    }

    public List<Equipment> GetEquipment()
    {
        List<Equipment> result = new List<Equipment>();
        result.Add(_weapon);
        result.Add(_armor);
        return result;
    }
}
