using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestZenject : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }

    [Test]
    public void GetGameManager()
    {
        var gm = Container.Resolve<GameManager>();

        Assert.That(gm != null);
    }
}