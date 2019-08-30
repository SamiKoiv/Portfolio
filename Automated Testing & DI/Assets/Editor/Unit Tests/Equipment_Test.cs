using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mocks;

namespace Tests
{
    public class Equipment_Test
    {
        [Test]
        public void EquipWeapon()
        {
            IWeapon weapon = new MockWeapon();
            Equipment equipment = new Equipment();

            equipment.EquipWith(weapon);
            Assert.Pass();
        }
    }
}
