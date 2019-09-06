using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestGameManager : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void CheckIfExist()
    {
        Assert.That(Container.Resolve<GameManager>() != null);
    }
}