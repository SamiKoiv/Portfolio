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
    public void EmptyTest()
    {
        
    }
}