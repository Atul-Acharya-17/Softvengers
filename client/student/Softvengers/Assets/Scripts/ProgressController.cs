using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController:MonoBehaviour
{
    public static int selectedUniverse=0;
    public static int selectedSs=0;
    public static int numUniverse = 6;
    public static int numSS = 4;
    public static Dictionary<int, Dictionary<string, string>> Multiverse_prog_info;
    public static Dictionary<int, Dictionary<int, Dictionary<string, string>>> Universe_prog_info;
    // after connecting to backend and processing, these are formats of dictionaries required
    //this is for each universe
    /*public static Dictionary<int, Dictionary<string, string>> Multiverse_prog_info =
 new Dictionary<int, Dictionary<string, string>>()
 {
        {
            0,
            new Dictionary<string, string>
            {
                {"name", "Universe 1"},
                {"value", "1"},
            }
        },
        {
            1,
            new Dictionary<string, string>
            {
                {"name", "Universe 2"},
                {"value", "1"},
            }
        },
        {
            2,
            new Dictionary<string, string>
            {
                {"name", "Universe 3"},
                {"value", "0.5"},
            }
        },
        {
            3,
            new Dictionary<string, string>
            {
                {"name", "Universe 4"},
                {"value", "0"},
            }
        },
        {
            4,
            new Dictionary<string, string>
            {
                {"name", "Universe 5"},
                {"value", "0"},
            }
        },
        {
            5,
            new Dictionary<string, string>
            {
                {"name", "Universe 6"},
                {"value", "0"},
            }
        },

 };


    //this is for each solar system


    public static Dictionary<int, Dictionary<int, Dictionary<string, string>>> Universe_prog_info =
        new Dictionary<int, Dictionary<int, Dictionary<string, string>>>()
        {   
            { 0,
                new Dictionary<int, Dictionary<string, string>>
                {
                               {
                                    0,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "Solar System 1"},
                                        {"value", "1"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    1,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "Solar System  2"},
                                        {"value", "1"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    2,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "Solar System  3"},
                                        {"value", "0.5"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    3,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "Solar System  4"},
                                        {"value", "0"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                }
            },


            { 1,
                new Dictionary<int, Dictionary<string, string>>
                {
                               {
                                    0,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "SS 1"},
                                        {"value", "1"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    1,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "SS  2"},
                                        {"value", "0.4"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    2,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "SS  3"},
                                        {"value", "0"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                                {
                                    3,
                                    new Dictionary<string, string>
                                    {
                                        {"name", "SS  4"},
                                        {"value", "0"},
                                        {"basic","0.33" },
                                        {"intermediate","0.22" },
                                        {"advanced","0" }
                                    }
                                },
                }
            },



        };*/

    /*[System.Serializable]
    public class PlanetProgress
    {
        public string identifier;
        public int maxCorrect;
       
    }

    [System.Serializable]
    public class Progress
    {
        public PlanetProgress[] progress;

    }



    public void Awake()
    {
        StartCoroutine(ServerController.Get("http://localhost:5000/student/details/getProgress?emailID=SRISH@e.ntu.edu.sg",
        result =>
        {
            if (result != null)
            {
                Progress FullProgress = JsonUtility.FromJson<Progress>(result);
                print(FullProgress.progress[0].identifier);
                print(FullProgress.progress.Length);

            }
            else
            {
                Debug.Log("No questions!");

            }
        }
        ));
    }*/
}
