using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class CountObjectsTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CountObjectsTestCountUniverses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            int count = gameObject.GetComponent<UniverseNavigation>().GetGameObjects().Count;
            int expectedCount = Multiverse.getUniverses().Count;

            Assert.AreEqual(expectedCount, count);
        }

        [UnityTest]
        public IEnumerator CountObjectsTestCountSolarSystem()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            gameObject.GetComponent<UniverseNavigation>().playerData.universePogress = 0;
            gameObject.GetComponent<UniverseNavigation>().playerData.solarSystemProgress = 0;
            gameObject.GetComponent<UniverseNavigation>().playerData.solarSystemProgress = 0;

            gameObject.GetComponent<UniverseNavigation>().ExploreWorld(0);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            int count = gameObject.GetComponent<SolarSystemNavigation>().GetGameObjects().Count;
            int expectedCount = Multiverse.getSolarSystems(0).Count;

            Assert.AreEqual(expectedCount, count);
        }
    }
}
