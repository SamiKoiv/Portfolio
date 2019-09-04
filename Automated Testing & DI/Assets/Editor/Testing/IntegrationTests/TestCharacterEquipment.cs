using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mocks;

namespace Tests
{
    public class TestCharacterEquipment
    {
        [Test]
        public void EquipWeapon()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();

            MockWeapon sword = new MockWeapon();
            character.EquipWith(sword);
            Assert.Pass();
        }

        [Test]
        public void EquipArmor()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();

            MockArmor armor = new MockArmor();
            character.EquipWith(armor);
            Assert.Pass();
        }

        [Test]
        public void GetEquipmentIDs()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();
            MockWeapon sword = new MockWeapon();
            MockArmor armor = new MockArmor();
            character.EquipWith(sword);
            character.EquipWith(armor);

            Equipment result = character.GetEquipment();
            Assert.That(result.Weapon.GetID() == sword.GetID() && result.Armor.GetID() == armor.GetID());
        }
    }
}
