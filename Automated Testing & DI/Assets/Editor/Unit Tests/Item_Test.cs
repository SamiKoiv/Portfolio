using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mocks;

namespace Tests
{
    public class Item_Test
    {
        [Test]
        public void ReturnName()
        {
            string testName = "Apple";

            Item item = ScriptableObject.CreateInstance<MockItem>();

            item.SetItem(testName);
            Assert.That(item.GetName().Equals(testName));
        }
    }
}
