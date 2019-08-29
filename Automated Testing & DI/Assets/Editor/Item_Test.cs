using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Item_Test
    {
        [Test]
        public void ReturnName()
        {
            string testName = "Apple";

            Item item = ScriptableObject.CreateInstance<Item>();

            item.SetItem(testName);
            Assert.That(item.GetName().Equals(testName));
        }
    }
}
