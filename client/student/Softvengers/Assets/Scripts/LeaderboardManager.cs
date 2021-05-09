using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System;

public class LeaderboardManager : MonoBehaviour
{
   
    public Transform entryContainer;
    public Transform entryTemplate;
    public Transform playerRank;
    private Dictionary<int, List<string>> leaders;

    public LeaderboardManager()
    {
        leaders = new Dictionary<int, List<string>>(){
            { 1, new List<string>(){"Player1", "1000" } },
            { 2, new List<string>(){"Player2", "900" } },
            { 3, new List<string>(){"Player3", "800" } },
            { 4, new List<string>(){"Player4", "700" } },
            { 5, new List<string>(){"Player5", "600" } },
            { 6, new List<string>(){"Player6", "500" } },
            { 7, new List<string>(){"Player7", "400" } },
            { 8, new List<string>(){"Player8", "300" } },
            { 9, new List<string>(){"Player9", "200" } },
            { 10, new List<string>(){"Player10", "100" } }
        };
    }

    private Transform AssignRank(Transform entryTransform, int val)
    {
        Transform rank = entryTransform.Find("Rank");
        Text t = rank.GetComponent<Text>();
        t.text = val.ToString();
        return entryTransform;
    }

    private Transform AssignUsername(Transform entryTransform, string name)
    {
        Transform username = entryTransform.Find("Username");
        Text t = username.GetComponent<Text>();
        t.text = name;
        return entryTransform;
    }

    private Transform AssignScore(Transform entryTransform, double val)
    {
        Transform score = entryTransform.Find("Score");
        Text t = score.GetComponent<Text>();
        t.text = val.ToString();
        return entryTransform;
    }

    private void Awake()
    {
        //entryContainer = transform.Find("Container");
        //entryTemplate = entryContainer.Find("Entry_template");

        //entryTemplate.gameObject.SetActive(false);

        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/details/getLeaderboard?emailID={0}", SecurityToken.Email),
            result =>
            {
                if (result != null)
                {
                    Debug.Log("Received result");
                    Debug.Log(result);
                    LeaderBoard leaderBoard = JsonUtility.FromJson<LeaderBoard>(result);

                    playerRank.GetComponent<Text>().text = "Your current rank: " + leaderBoard.myRank.ToString();

                    float templateHeight = 120f;

                    for (int i = 1; i <= leaderBoard.students.Count; i++)
                    {
                        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                        entryTransform.transform.SetParent(GameObject.FindGameObjectWithTag("RankPage").transform, false);
                        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                        entryTransform = AssignRank(entryTransform, i);
                        string name = leaderBoard.students[i - 1].firstName + " " + leaderBoard.students[i - 1].lastName;
                        entryTransform = AssignUsername(entryTransform, name);
                        entryTransform = AssignScore(entryTransform, Math.Round(leaderBoard.students[i-1].totalScore, 3));
                        entryTransform.gameObject.SetActive(true);

                    }
                }
            }
            ));
    }
    
}

[System.Serializable]
public class LeaderBoard
{
    public int myRank;
    public List<Student> students;
}

[System.Serializable]
public class Student
{
    public string firstName;
    public string lastName;
    public double totalScore;
}