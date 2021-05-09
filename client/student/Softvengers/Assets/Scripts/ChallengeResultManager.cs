using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeResultManager : MonoBehaviour
{
    private static double score = 0;
    private static string challenge;
    public GameObject resultRecord;

    void Start()
    {
        challenge = Challenge.challengeName;
        UpdateProgress();
        float x = 0;
        float y = 200.0f;
        float z = 0;

        GameObject title = Instantiate(resultRecord, new Vector3(x, y, z), Quaternion.identity);
        title.transform.SetParent(GameObject.FindGameObjectWithTag("ChallengeResultPage").transform, false);
        Transform challengeName = title.transform.Find("ChallengeName");
        Transform res = title.transform.Find("Score");
        challengeName.GetComponent<Text>().text = "CHALLENGE NAME";
        res.GetComponent<Text>().text = "SCORE";


        y -= 50.0f;
        GameObject result = Instantiate(resultRecord, new Vector3(x, y, z), Quaternion.identity);
        result.transform.SetParent(GameObject.FindGameObjectWithTag("ChallengeResultPage").transform, false);
        challengeName = result.transform.Find("ChallengeName");
        res = result.transform.Find("Score");
        challengeName.GetComponent<Text>().text = challenge;
        res.GetComponent<Text>().text = System.Math.Round(score, 2).ToString();

    }

    public static void UpdateScore(double val)
    {
        score += val;
    }

    public static double getScore()
    {
        return score;
    }

    public static void setChallengeName(string s)
    {
        challenge = s;
    }

    public static string getChallengeName()
    {
        return challenge;
    }

    void UpdateProgress()
    {
        //TODO Set the current challenge as completed in database
        ChallengeResult challengeResult = new ChallengeResult();
        challengeResult.challengeID = Challenge.challengeID;
        challengeResult.emailID = SecurityToken.Email;
        challengeResult.receiverScore = score;
        challengeResult.receiverTime = ChallengeGameController.endTime - ChallengeGameController.startTime;

        StartCoroutine(ServerController.Put("http://localhost:5000/student/challenge/endChallenge", challengeResult.stringify(),
        result =>
        {
            Debug.Log(result);
            Debug.Log("Done");
        }
        ));
    }


}

public class ChallengeResult
{
    public string challengeID;
    public string emailID;
    public double receiverScore;
    public float receiverTime;

    public string stringify()
    {
        return JsonUtility.ToJson(this);
    }
}