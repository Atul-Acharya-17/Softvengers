using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * _id
:
605723d04178e834a4d4b2a3
wrongOptions:Array
points:15
universeID:0
solarID:0
planetID:2
questionID:1
body:"What does MERN mean?"
correctOption:"Mongo, Express, React, Node"
__v:0
 */
//needs to be tested, was working with empty db
[System.Serializable]
public class ChaQuestion
{
    //question structure for challenge
    public string _id;
    public string[] wrongOptions;
    public int points;
    public int universeID;
    public int solarID;
    public int planetID;
    public int questionID;
    public string body;
    public string correctOption;
    public int _v;

}
[System.Serializable]
public class ChaQuestions
{
    public ChaQuestion[] questions;
}


public class ChallengeGameController : AssignmentGameController {
    public static float startTime;
    public static float endTime;
    private ChaQuestions qSet;

    public override bool IsGameOver()
    {
        if(base.IsGameOver())
        {
            endTime = Time.time;

        }
        return (base.IsGameOver());
    }

    
    public override double CalculateScore(bool result, double baseScore)
    {
        if (result)
        {
            return baseScore * (1 + ratio);
        }

        return 0.0;
    }

    public override void StoreScore(bool result, double score)
    {
        //ResultManager.AddRecord(result, score);
        ChallengeResultManager.UpdateScore(score);
    }

    protected override void Start()
    {
        paused = true;
        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/challenge/getQuestions?challengeID={0}", Challenge.challengeID),
        result =>
        {
            if (result != null)
            {
                Debug.Log(result);

                qSet = JsonUtility.FromJson<ChaQuestions>("{ \"questions\": " + result + "}");
                print(qSet.questions.Length);
                foreach (ChaQuestion q in qSet.questions)
                {
                    print(q.body);
                }

                initQstBank();
                numQuestions = questionBank.Count;
                startTime = Time.time;
                DisplayQuestion();

            }
            else
            {
                Debug.Log("No sent challenges");

            }
        }
        ));
    }

    

    // Initialize

    public override void initQstBank()
    {
        for (int i = 0; i < qSet.questions.Length; i++)
        {
            string body = qSet.questions[i].body;
            int points = qSet.questions[i].points;
            List<Option> ol = new List<Option>();
            Option corr = new Option(qSet.questions[i].correctOption, true);
            ol.Add(corr);
            for (int j = 0; j < 3; j++)
            {
                Option incorr = new Option(qSet.questions[i].wrongOptions[j], false);
                ol.Add(incorr);
            }
            Question q = new Question(body, ol, points);
            this.questionBank.Add(q);
        }
    }

    public ChallengeGameController()
    {
        this.nextScene = "ChallengeResultScene";
    }
}
