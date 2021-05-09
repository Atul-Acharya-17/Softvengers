using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<bool> results = new List<bool>();
    public static List<double> scores = new List<double>();
    public GameObject resultRecord;
    public Transform title;
    public Player playerData;
    public Navigation navigation;

    void Awake()
    {
        SendData();

        var numQuery = results.Where(result => result == true);
        Debug.Log(results.Count);

        if (numQuery.Count() > 5) // Pass
        {
            UpDatePlayerProgress(playerData, navigation);

            if (playerData != null)
            {
                Results newProgress = new Results();
                newProgress.emailID = SecurityToken.Email;
                newProgress.conqueredPlanet = playerData.planetProgress;
                newProgress.conqueredSolarSystem = playerData.solarSystemProgress;
                newProgress.conqueredUniverse = playerData.universePogress;


                StartCoroutine(ServerController.Put("http://localhost:5000/student/game/unlock", newProgress.stringify(), result =>
                {
                    if (result != null)
                    {
                        Debug.Log("Updated Successfully");
                    }
                    else
                    {
                        Debug.Log("Error");
                    }
                }
                ));
            }
        }
    }
    

    void Start()
    {
        
        float startY = 200.0f;
        float x = 0;
        float z = 0;
        //Debug.Log(scores.Count);
        //Debug.Log(results.Count);
        double sum = 0;
        for (int i = 0; i < scores.Count; ++i)
            sum += scores[i];

        if (title != null)
        {
            title.GetComponent<Text>().text = string.Format("Final Score : {0}", Math.Round(sum, 2));
            for (int i = 0; i < results.Count; ++i)
            {
                GameObject result = Instantiate(resultRecord, new Vector3(x, startY, z), Quaternion.identity);
                result.transform.SetParent(GameObject.FindGameObjectWithTag("ResultPage").transform, false);
                Transform questionNumber = result.transform.Find("QuestionNumber");
                Transform outcome = result.transform.Find("Outcome");
                Transform score = result.transform.Find("Score");
                questionNumber.GetComponent<Text>().text = string.Format("Question {0}", i + 1);
                outcome.GetComponent<Text>().text = results[i] ? "Correct" : "Wrong";
                outcome.GetComponent<Text>().color = results[i] ? new Color(0, 1, 0) : new Color(1, 0, 0);

                score.GetComponent<Text>().text = Math.Round(scores[i], 2).ToString();
                startY -= 60.0f;
            }

            for (int i = results.Count; i < 10; i++)
            {
                GameObject result = Instantiate(resultRecord, new Vector3(x, startY, z), Quaternion.identity);
                result.transform.SetParent(GameObject.FindGameObjectWithTag("ResultPage").transform, false);
                Transform questionNumber = result.transform.Find("QuestionNumber");
                Transform outcome = result.transform.Find("Outcome");
                questionNumber.GetComponent<Text>().text = string.Format("Question {0}", i + 1);
                outcome.GetComponent<Text>().text = "Not Attempted";
                outcome.GetComponent<Text>().color = new Color(0.4f, 0.4f, 0.4f);
                startY -= 60.0f;
            }
        }
        
    }

    public void BackButton()
    {
        results = new List<bool>();
        scores = new List<double>();
        SceneManager.LoadScene("PlanetScene");
    }

    public static void AddRecord(bool result, double score)
    {
        Debug.Log(results.Count + 1);
        results.Add(result);
        Debug.Log(results.Count + 1);
        scores.Add(score);
    }

    void SendData()
    {
       if (navigation == null)
        {
            return;
        }
        
       StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/details/getMaxScore/?emailID={0}&universe={1}&SolarSystem={2}&planet={3}", SecurityToken.Email, navigation.universeSelected, navigation.solarSystemSelected, navigation.planetSelected), 
       result => {
       double maxScore;
       double score = 0.0;
       if (result != null){
               maxScore = Double.Parse(result);
               var numQuery = results.Where(outcome => outcome == true);
               if (numQuery.Count() > 5)
               {
                   for (int i = 0; i < scores.Count; ++i)
                       score += scores[i];
               }
               if (score > maxScore)
               {
                   playerData.totalScore += (score - maxScore);
               }
            }

           FinalScores finalScores = new FinalScores();
           finalScores.emailID = SecurityToken.Email;
           finalScores.universe = navigation.universeSelected;
           finalScores.SolarSystem = navigation.solarSystemSelected;
           finalScores.planet = navigation.planetSelected;
           finalScores.score = score;
           finalScores.correctAnswers = results.Where(outcome => outcome == true).Count();
           finalScores.totalScore = playerData.totalScore;


           StartCoroutine(ServerController.Put("http://localhost:5000/student/game/endGame", finalScores.stringify(),
               message =>
               {
                   if (message != null)
                   {
                       Debug.Log(message);
                   }
                   else
                   {
                       Debug.Log("Failed to update");
                   }
               }));
       }
       ));

    }

    public void UpDatePlayerProgress(Player playerData, Navigation navigation)
    {
        if (playerData == null || navigation == null)
            return;

        if (IsLatestLevel(playerData, navigation))
        {
            if (playerData.planetProgress < 2) // Not last planet
            {
                playerData.planetProgress += 1;
            }
            else if (playerData.solarSystemProgress < Multiverse.getSolarSystems(playerData.universePogress).Count - 1) // Check if player didnt exceed max solar systems of the universe 
            {
                playerData.solarSystemProgress += 1;
                playerData.planetProgress = 0;
            }
            else if (playerData.universePogress < Multiverse.getUniverses().Count)// Check if player has not exceeded total num universes
            {
                playerData.universePogress += 1;
                playerData.solarSystemProgress = 0;
                playerData.planetProgress = 0;
            }
        }
    }

    bool IsLatestLevel(Player playerData, Navigation navigation)
    {
        if (navigation.universeSelected == playerData.universePogress && navigation.solarSystemSelected == playerData.solarSystemProgress && playerData.planetProgress == navigation.planetSelected)
            return true;
        return false;
    }
}

public class Results
{
    public string emailID;
    public int conqueredUniverse;
    public int conqueredSolarSystem;
    public int conqueredPlanet;

    public string stringify()
    {

        //return JsonUtility.ToJson(this);
        //var dic = "{'username': this.username, 'password': this.password}";
        return JsonUtility.ToJson(this);
    }
}

public class FinalScores
{
    public string emailID;
    public int universe;
    public int SolarSystem;
    public int planet;
    public double score;
    public int correctAnswers;
    public double totalScore;

    public string stringify()
    {

        //return JsonUtility.ToJson(this);
        //var dic = "{'username': this.username, 'password': this.password}";
        return JsonUtility.ToJson(this);
    }
}
