using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestZenject : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void GetGameManager()
    {
        var gm = Container.Resolve<GameManager>();

        Assert.That(gm != null);
    }
}