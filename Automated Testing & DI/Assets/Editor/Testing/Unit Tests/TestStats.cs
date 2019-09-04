using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestStats
    {
        [Test]
        public void CreateAndVerifyStats()
        {
            int hp = 1;
            int attack = 2;
            int defence = 3;
            Stats stats = new Stats(hp, attack, defence);

            Assert.That(stats.HP == hp && stats.Attack == attack && stats.Defence == defence);
        }
    }
}
