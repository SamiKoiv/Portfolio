using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Inventory_Test
    {
        [Test]
        public void Contain()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<Item>();

            inventory.HandItem(testItem);
            Assert.That(inventory.ContainsItem(testItem));
        }

        [Test]
        public void RightContainedAmount()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<Item>();

            int handedAmount = 3;
            bool containsItem;
            int containedAmount;

            for(int i = 0; i < handedAmount; i++)
            {
                inventory.HandItem(testItem);
            }

            containsItem = inventory.ContainsItem(testItem, out containedAmount);

            Assert.That(containsItem && containedAmount == handedAmount);
        }

        [Test]
        public void HandleReduction()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<Item>();

            int handedAmount = 3;
            int reduction = 2;
            int result = handedAmount - reduction;

            bool containsItem;
            int containedAmount;

            for (int i = 0; i < handedAmount; i++)
            {
                inventory.HandItem(testItem);
            }

            for (int i = 0; i < reduction; i++)
            {
                inventory.TakeItem(testItem);
            }

            containsItem = inventory.ContainsItem(testItem, out containedAmount);

            Assert.That(containsItem && containedAmount == result);
        }

        [Test]
        public void HandleInvalidReduction()
        {
            Inventory inventory = new Inventory();
            Item testItem = ScriptableObject.CreateInstance<Item>();

            int handedAmount = 3;
            int reduction = handedAmount + 1;
            int result = 0;

            bool containsItem;
            int containedAmount;

            for (int i = 0; i < handedAmount; i++)
            {
                inventory.HandItem(testItem);
            }

            for (int i = 0; i < reduction; i++)
            {
                inventory.TakeItem(testItem);
            }

            containsItem = inventory.ContainsItem(testItem, out containedAmount);

            Assert.That(!containsItem && containedAmount == result);
        }
    }
}
