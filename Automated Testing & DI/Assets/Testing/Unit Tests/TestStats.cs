using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestStats : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void CreateAndVerifyStats()
    {
        int Vitality = 1;
        int Strength = 2;
        int Dexterity = 3;
        int Intelligence = 4;
        int PhysicalDefence = 5;
        int PhysicalResistance = 6;
        int MagicDefence = 7;
        int MagicResistance = 8;
        int AttackSpeed = 9;
        int CritChance = 10;
        int CritDamage = 11;
        int LifeLeech = 12;

        Stats stats = new Stats
        {
            Vitality = Vitality,
            Strength = Strength,
            Dexterity = Dexterity,
            Intelligence = Intelligence,
            PhysicalDefence = PhysicalDefence,
            PhysicalResistance = PhysicalResistance,
            MagicDefence = MagicDefence,
            MagicResistance = MagicResistance,
            AttackSpeed = AttackSpeed,
            CritChance = CritChance,
            CritDamage = CritDamage,
            LifeLeech = LifeLeech
        };

        Assert.That(stats.Vitality == Vitality);
        Assert.That(stats.Strength == Strength);
        Assert.That(stats.Dexterity == Dexterity);
        Assert.That(stats.Intelligence == Intelligence);
        Assert.That(stats.PhysicalDefence == PhysicalDefence);
        Assert.That(stats.PhysicalResistance == PhysicalResistance);
        Assert.That(stats.MagicDefence == MagicDefence);
        Assert.That(stats.MagicResistance == MagicResistance);
        Assert.That(stats.AttackSpeed == AttackSpeed);
        Assert.That(stats.CritChance == CritChance);
        Assert.That(stats.CritDamage == CritDamage);
        Assert.That(stats.LifeLeech == LifeLeech);
    }
}