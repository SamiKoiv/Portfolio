using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestCharacter : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void EmptyTest()
    {
        Assert.That(true);
    }
}