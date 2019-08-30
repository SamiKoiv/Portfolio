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
    }
}
