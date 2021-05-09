using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Multiverse
{
    /// <summary>
    /// Delete everything below
    /// </summary>

    private static int UniverseSelected = 0;
    private static int SolarSystemSelected = 0;

    private static bool isPaused = false;

    private static int playerUniverse = 1;
    private static int playerSS = 0;
    private static int playerPlanet = 0;
    ///
    /// Get multiverse dictionary
    ///

    private static Dictionary<string, List<string>> multiverse = new Dictionary<string, List<string>>()
    {
        {
            "Requirement Engineering", new List<string>()
            {
                "Requirement Elicitation", "Requirement Analysis", "Requirement Specification", "Requirement Verification"
            }
        },

        {
            "Software Design", new List<string>()
            {
                "Design Principles", "Design Process", "Design Strategies"
            }
        },

        {
            "Software Implementation", new List<string>()
            {
                "Abstraction", "Encapsulation", "Inheritance", "Polymorphism"
            }
        },
        {
            "Software Testing", new List<string>()
            {
                "Unit Test", "Integration Test"
            }
        },
        {
            "Software Deployment", new List<string>()
            {
                "AWS", "GCP", "Heroku"
            }
        },
        {
            "Software Maintenance", new List<string>()
            {
                "Management", "Maintenance"
            }
        }
    };

    public static List<string> getUniverses()
    {
        return multiverse.Keys.ToList();
    }

    public static List<string> getSolarSystems(int universeID)
    {
        return multiverse[multiverse.Keys.ElementAt(universeID)];
    }

}
