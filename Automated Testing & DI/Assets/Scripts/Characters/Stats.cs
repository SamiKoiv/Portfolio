using UnityEngine;
// Extended with Add function (see StatsExtensions script)

    [System.Serializable]
public struct Stats
{
    [Header("Primary Attributes")]
    public int Vitality;
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    // Perception?

    [Header("Defensive Bonus")]
    public int PhysicalDefence;
    public float PhysicalResistance; // %
    public int MagicDefence;
    public float MagicResistance; // %

    [Header("Offensive Bonus")]
    public float AttackSpeed;
    // Accuracy?
    // Evasion?
    public float CritChance; // %
    public float CritDamage; // %
    public float LifeLeech; // %

    public int MaxHp => 100 * Vitality;
    public float AttackRate => 1 / AttackSpeed;
}
