using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestItem : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void ReturnName()
    {
        string testName = "Apple";

        Item item = Container.Resolve<Item>();

        item.SetItem(testName);
        Assert.That(item.GetName().Equals(testName));
    }
}