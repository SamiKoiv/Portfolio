using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, ICharacter, IGetName, IEquipWith
{
    protected string m_name;

    Equipment m_equipment = new Equipment();
    IWeapon m_weapon;
    IArmor m_armor;

    public virtual string GetName()
    {
        return m_name;
    }

    public Equipment GetEquipment()
    {
        return m_equipment;
    }

    public void EquipWith(IWeapon weapon)
    {
        m_equipment.EquipWith(weapon);
    }

    public void EquipWith(IArmor armor)
    {
        m_equipment.EquipWith(armor);
    }
}
