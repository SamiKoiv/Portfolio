using Zenject;
using NUnit.Framework;
using Skills;

[TestFixture]
public class TestSkills : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }



[Test]
    public void TestPound()
    {
        ICharacter characterA = Container.Resolve<ICharacter>();
        ICharacter characterB = Container.Resolve<ICharacter>();

        int hp = characterA.CurrentHP.Value;

        ISkill pound = new Pound();
        pound.Initialize(characterB, characterA);
        characterA.ApplySkill(pound);

        Assert.That(characterA.CurrentHP.Value == characterA.Stats_Total.MaxHp - characterB.Stats_Total.Strength);
    }
}