using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ProgressTest
    {
        // A Test behaves as an ordinary method
        private Player player;
        private Navigation navigation;

        [Test]
        public void ProgressTestLockWorld()
        {
            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 1;
            player.solarSystemProgress = 2;
            player.planetProgress = 2;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 1;
            navigation.planetSelected = 2;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

            Assert.AreEqual(1, player.universePogress);
            Assert.AreEqual(2, player.solarSystemProgress);
            Assert.AreEqual(2, player.planetProgress);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [Test]
        public void ProgressTestNewPlanetUnlocked()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 0;
            player.solarSystemProgress = 0;
            player.planetProgress = 0;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 0;
            navigation.planetSelected = 0;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

            Assert.AreEqual(0, player.universePogress);
            Assert.AreEqual(0, player.solarSystemProgress);
            Assert.AreEqual(1, player.planetProgress);
        }

        [Test]
        public void ProgressTestNewSolarSystemUnlocked()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 0;
            player.solarSystemProgress = 0;
            player.planetProgress = 3;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 0;
            navigation.planetSelected = 3;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

            Assert.AreEqual(0, player.universePogress);
            Assert.AreEqual(1, player.solarSystemProgress);
            Assert.AreEqual(0, player.planetProgress);
        }

        [Test]
        public void ProgressTestNewUniverseUnlocked()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 0;
            player.solarSystemProgress = 3;
            player.planetProgress = 2;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 3;
            navigation.planetSelected = 2;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

           
            Assert.AreEqual(1, player.universePogress);
            Assert.AreEqual(0, player.solarSystemProgress);
            Assert.AreEqual(0, player.planetProgress);
        }
    }
}
