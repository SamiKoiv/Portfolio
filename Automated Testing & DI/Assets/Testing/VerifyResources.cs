using UnityEngine;
using Zenject;
using NUnit.Framework;

[TestFixture]
public class VerifyResources : ZenjectUnitTestFixture
{
    [Test]
    public void VerifyMainInstaller()
    {
        MainInstaller resource = Resources.Load<MainInstaller>("MainInstaller");
        Assert.IsNotNull(resource);
    }

    [Test]
    public void VerifyGameParameters()
    {
        GameParameters resource = Resources.Load<GameParameters>("GameParameters");
        Assert.IsNotNull(resource);
    }

    [Test]
    public void VerifyItemDatabase()
    {
        ItemDatabase resource = Resources.Load<ItemDatabase>("ItemDatabase");
        Assert.IsNotNull(resource);
    }
}