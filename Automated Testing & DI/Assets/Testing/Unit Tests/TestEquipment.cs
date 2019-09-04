using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestEquipment : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void EquipWeapon()
    {
        IWeapon weapon = Container.Resolve<IWeapon>();
        Equipment equipment = new Equipment();

        equipment.EquipWith(weapon);
        Assert.Pass();
    }
}