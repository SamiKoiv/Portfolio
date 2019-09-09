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
        IItem testItem = Container.Resolve<IItem>();

        inventory.Deposit(testItem.GetID());
        Assert.That(inventory.Contains(testItem.GetID()));
    }

    [Test]
    public void ContainsRightAmount()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        IItem testItem = Container.Resolve<IItem>();

        int deposit = 3;
        bool containsItem;
        int containedAmount;

        inventory.Deposit(testItem.GetID(), deposit);
        containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

        Assert.That(containsItem && containedAmount == deposit);
    }

    [Test]
    public void ReduceMany()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        IItem testItem = Container.Resolve<IItem>();

        int deposit = 3;
        int withdraw = 2;
        int result = deposit - withdraw;

        bool containsItem;
        int containedAmount;

        inventory.Deposit(testItem.GetID(), deposit);
        inventory.Withdraw(testItem.GetID(), withdraw);
        containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

        Assert.That(containsItem && containedAmount == result);
    }

    [Test]
    public void ReduceTooMuch()
    {
        IInventory inventory = Container.Resolve<IInventory>();
        IItem testItem = Container.Resolve<IItem>();

        Assert.That(inventory.Withdraw(testItem.GetID()) == false);
    }
}