using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


class AssignmentResults
{
    public int assignmentID;
    public string matricNo;
    public double scores;

    public string stringify()
    {
        return JsonUtility.ToJson(this);
    }
}

public class AssignmentResultManager : MonoBehaviour
{
    public static string assignment = "Assignment 1";
    public static int assignmentID = AssignmentScene.selectedAssignmentid;
    public GameObject resultRecord;
    public Transform title;

    public static List<bool> results = new List<bool>();
    public static List<double> scores = new List<double>();

    void Awake()
    {
        
        UpDatePlayerProgress();

    }

    void Start()
    {

        float startY = 150.0f;
        float x = 0;
        float z = 0;

        double sum = 0;
        for (int i = 0; i < scores.Count; ++i)
            sum += scores[i];

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
    }

    public void BackButton()
    {
        results = new List<bool>();
        scores = new List<double>();
        SceneManager.LoadScene("Assignment");
    }

    public static void AddRecord(bool result, double score)
    {
        Debug.Log(results.Count + 1);
        results.Add(result);
        scores.Add(score);
    }


    void UpDatePlayerProgress()
    {

        AssignmentResults newProgress = new AssignmentResults();
        newProgress.matricNo = SecurityToken.MatricNo;
        newProgress.assignmentID = assignmentID;
        newProgress.scores = scores.Sum();

        Debug.Log("Sending" + newProgress.assignmentID);
        StartCoroutine(ServerController.Put("http://localhost:5000/student/assignments/assignmentComplete", newProgress.stringify(), result =>
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
