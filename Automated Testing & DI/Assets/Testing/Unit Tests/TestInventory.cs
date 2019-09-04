using Zenject;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class TestInventory : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void Contain()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        Item testItem = Container.Resolve<Item>();

        inventory.Add(testItem.GetID());
        Assert.That(inventory.Contains(testItem.GetID()));
    }

    [Test]
    public void RightContainedAmount()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        Item testItem = Container.Resolve<Item>();

        int handedAmount = 3;
        bool containsItem;
        int containedAmount;

        for (int i = 0; i < handedAmount; i++)
        {
            inventory.Add(testItem.GetID());
        }

        containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

        Assert.That(containsItem && containedAmount == handedAmount);
    }

    [Test]
    public void HandleReduction()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        Item testItem = Container.Resolve<Item>();

        int quantity;
        bool hasItem = inventory.Contains(testItem.GetID(), out quantity);

        int handedAmount = 3;
        int reduction = 2;
        int result = handedAmount - reduction;

        bool containsItem;
        int containedAmount;

        for (int i = 0; i < handedAmount; i++)
        {
            inventory.Add(testItem.GetID());
        }

        hasItem = inventory.Contains(testItem.GetID(), out quantity);

        for (int i = 0; i < reduction; i++)
        {
            inventory.Reduce(testItem.GetID());
        }

        hasItem = inventory.Contains(testItem.GetID(), out quantity);

        containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

        Assert.That(containsItem && containedAmount == result);
    }

    [Test]
    public void HandleInvalidReduction()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        Item testItem = Container.Resolve<Item>();

        Assert.That(inventory.Reduce(testItem.GetID()) == false);
    }
}