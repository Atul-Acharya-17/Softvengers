using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class LockedWorldTest
    {
        [UnityTest]
        public IEnumerator LockedWorldTestLockedUniverse()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 1;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(2);
            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("UniverseScene", currentScene);

        }


        [UnityTest]
        public IEnumerator LockedWorldTestUnlockedUniverse()
        {
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 3;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(2);
            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("SolarSystemScene", currentScene);
        }


        [UnityTest]
        public IEnumerator LockedWorldTestLockedSolarSystem()
        {
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 3;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(3);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            SolarSystemNavigation solarSystemNavigation = gameObject.GetComponent<SolarSystemNavigation>();

            solarSystemNavigation.playerData.universePogress = 3;
            solarSystemNavigation.playerData.solarSystemProgress = 0;
            solarSystemNavigation.playerData.planetProgress = 0;

            solarSystemNavigation.ExploreWorld(1);
            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("SolarSystemScene", currentScene);
        }


        [UnityTest]
        public IEnumerator LockedWorldTestUnlockedSolarSystem()
        {
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 3;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(3);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            SolarSystemNavigation solarSystemNavigation = gameObject.GetComponent<SolarSystemNavigation>();

            solarSystemNavigation.playerData.universePogress = 3;
            solarSystemNavigation.playerData.solarSystemProgress = 0;
            solarSystemNavigation.playerData.planetProgress = 0;

            solarSystemNavigation.ExploreWorld(0);
            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("PlanetScene", currentScene);
        }


        [UnityTest]
        public IEnumerator LockedWorldTestLockedPlanet()
        {
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 3;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(3);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            SolarSystemNavigation solarSystemNavigation = gameObject.GetComponent<SolarSystemNavigation>();

            solarSystemNavigation.playerData.universePogress = 3;
            solarSystemNavigation.playerData.solarSystemProgress = 0;
            solarSystemNavigation.playerData.planetProgress = 0;

            solarSystemNavigation.ExploreWorld(0);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            PlanetNavigation planetNavigation = gameObject.GetComponent<PlanetNavigation>();

            planetNavigation.playerData.universePogress = 3;
            planetNavigation.playerData.solarSystemProgress = 0;
            planetNavigation.playerData.planetProgress = 0;

            planetNavigation.ExploreWorld(1);

            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("PlanetScene", currentScene);

        }

        [UnityTest]
        public IEnumerator LockedWorldTestUnlockedPlanet()
        {
            GameObject gameObject;
            SceneManager.LoadScene("UniverseScene");
            yield return new WaitForSeconds(1.0f);


            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            UniverseNavigation universeNavigation = gameObject.GetComponent<UniverseNavigation>();

            universeNavigation.playerData.universePogress = 3;
            universeNavigation.playerData.solarSystemProgress = 0;
            universeNavigation.playerData.planetProgress = 0;

            universeNavigation.ExploreWorld(3);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            SolarSystemNavigation solarSystemNavigation = gameObject.GetComponent<SolarSystemNavigation>();

            solarSystemNavigation.playerData.universePogress = 3;
            solarSystemNavigation.playerData.solarSystemProgress = 0;
            solarSystemNavigation.playerData.planetProgress = 0;

            solarSystemNavigation.ExploreWorld(0);
            yield return new WaitForSeconds(1.0f);

            gameObject = GameObject.Find("Navigator");
            Assert.NotNull(gameObject);

            PlanetNavigation planetNavigation = gameObject.GetComponent<PlanetNavigation>();

            planetNavigation.playerData.universePogress = 3;
            planetNavigation.playerData.solarSystemProgress = 0;
            planetNavigation.playerData.planetProgress = 0;

            bool unlocked = planetNavigation.IsUnlocked(0);

            Assert.AreEqual(true, unlocked);
        }
    }
}
