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
        int hp = 1;
        int attack = 2;
        int defence = 3;
        Stats stats = new Stats(hp, attack, defence);

        Assert.That(stats.HP == hp && stats.Attack == attack && stats.Defence == defence);
    }
}