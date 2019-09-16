using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestCharacterFactory : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void Injection()
    {
        ICharacterFactory factory = Container.Resolve<ICharacterFactory>();
        Assert.That(factory != null);
    }

    [Test]
    public void CreateCharacter()
    {
        ICharacterFactory factory = Container.Resolve<ICharacterFactory>();
        string name = "TestName";
        Stats stats = Container.Resolve<Stats>();
        ICharacter character = factory.Next(name, stats);

        Assert.That(character.Name.Equals(name));
        Assert.That(character.Stats_Base.Strength == stats.Strength);
    }
}