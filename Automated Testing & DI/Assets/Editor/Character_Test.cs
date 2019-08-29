using Mocks;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class Character_Test
    {
        [Test]
        public void GetCharacterName()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();
            string testName = "Homer";
            character.MockName = testName;
            string cname = character.GetName();
            Assert.That(cname.Equals(testName));
        }

        [Test]
        public void EquipWeapon()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();

            MockWeapon sword = new MockWeapon();
            string mockName = "Greatsword";
            sword.MockName = mockName;

            character.EquipWith(sword);
            Assert.Pass();
        }

        [Test]
        public void GetEquipment()
        {
            MockCharacter character = new GameObject("Test Character").AddComponent<MockCharacter>();

            MockWeapon sword = new MockWeapon();
            string mockName = "Greatsword";
            sword.MockName = mockName;

            character.EquipWith(sword);
            List<Equipment> equipment = character.GetEquipment();
            Assert.That(equipment.Contains(sword));
        }
    }
}
