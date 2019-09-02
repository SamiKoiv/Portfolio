using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mocks;

namespace Tests
{
    public class Inventory_Test
    {
        [Test]
        public void Contain()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<MockItem>();

            inventory.Store(testItem);
            Assert.That(inventory.Contains(testItem.GetID()));
        }

        [Test]
        public void RightContainedAmount()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<MockItem>();

            int handedAmount = 3;
            bool containsItem;
            int containedAmount;

            for(int i = 0; i < handedAmount; i++)
            {
                inventory.Store(testItem);
            }

            containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

            Assert.That(containsItem && containedAmount == handedAmount);
        }

        [Test]
        public void HandleReduction()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<MockItem>();

            int handedAmount = 3;
            int reduction = 2;
            int result = handedAmount - reduction;

            bool containsItem;
            int containedAmount;

            for (int i = 0; i < handedAmount; i++)
            {
                inventory.Store(testItem);
            }

            for (int i = 0; i < reduction; i++)
            {
                inventory.Withdraw(testItem.GetID());
            }

            containsItem = inventory.Contains(testItem.GetID(), out containedAmount);

            Assert.That(containsItem && containedAmount == result);
        }

        [Test]
        public void HandleInvalidReduction()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<MockItem>();

            Assert.Throws<InventoryException>(() => inventory.Withdraw(testItem.GetID()));
        }
    }
}
