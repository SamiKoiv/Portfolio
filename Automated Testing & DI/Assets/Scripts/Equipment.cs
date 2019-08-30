public struct Equipment : IEquipWith, IUnequip
{
    IWeapon m_weapon;
    IArmor m_armor;

    public IWeapon Weapon { get { return m_weapon; } }
    public IArmor Armor { get { return m_armor; } }

    public void EquipWith(IWeapon weapon)
    {
        m_weapon = weapon;
    }

    public void EquipWith(IArmor armor)
    {
        m_armor = armor;
    }

    public IArmor UnequipArmor()
    {
        IArmor result = m_armor;
        m_armor = null;
        return result;
    }

    public IWeapon UnequipWeapon()
    {
        IWeapon result = m_weapon;
        m_weapon = null;
        return result;
    }
}
