using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mocks;

namespace Tests
{
    public class IGetID_IntegrationTest
    {
        [Test]
        public void GetWeaponID()
        {
            IGetID testWeapon = new MockWeapon();
            Assert.That(testWeapon.GetID() == 000);
        }
    }
}
