using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
   {
   wrongOptions: [ 'Dance', 'With', 'You' ],
   points: 1,
   _id: 6062ab4fb5e20c11b8b8878c,
   assignmentID: 123,
   questionID: 3,
   body: 'My',
   correctOption: 'Again',
   __v: 0
 }
   */

[System.Serializable]
public class AssQuestion
{
    public string[] wrongOptions;
    public int points;
    public string _id;
    public int assignmentID;
    public int questionID;
    public string body;
    public string correctOption;
    public int _v;
}
[System.Serializable]
public class AssQuestions
{
    public AssQuestion[] questions;
}

public class AssignmentGameController : MonoBehaviour
{

    // Question Details on Screen
    public Transform questionName;
    public Transform option1;
    public Transform option2;
    public Transform option3;
    public Transform option4;

    // Question Time Limit
    public Image timeBar;

    // Asteroid for each question
    public GameObject asteroid;

    // Asteroids in Game
    private GameObject fragment;
    private GameObject currAsteroid;

    // Time Delay between each question 
    protected float timeDelay = 1.25f;

    // Question Time Limits

    private float startTime = 0.0f;


    // *************************************************************************************
    // Change to Data from server
    // *************************************************************************************
    private float questionTime = 7f;


    // Synchronization
    protected bool failedQuestion = false;
    protected bool isLocked = false;
    protected bool paused = false;

    // Time ratio
    protected float ratio;

    // Game Questions

    // *************************************************************************************
    // Once you receive data from server get the questions from that and place in List<Questions>
    // *************************************************************************************

    protected List<Question> questionBank = new List<Question>();
    protected int numQuestions;
    protected int questionNumber = 0;
    private AssQuestions qSet;
    protected string nextScene = "AssignmentResultScene";


    protected virtual void Start()
    {
    paused = true;
    StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/assignments/getAssignmentQuestions?assignmentID={0}", AssignmentScene.selectedAssignmentid),
        result =>
        {
            if (result != null)
            {
                Debug.Log("Hello1");
                Debug.Log(result);
                Debug.Log("Hello2");
                qSet = JsonUtility.FromJson<AssQuestions>("{ \"questions\": " + result + "}");
                Debug.Log("Number of questions: " + qSet.questions.Length);
                foreach(AssQuestion q in qSet.questions)
                {
                    print(q.body);
                }

                initQstBank();
                numQuestions = questionBank.Count;
                DisplayQuestion();

            }
            else
            {
                Debug.Log("No sent challenges");

            }
        }
        ));
    }

    void Update()
    {
        Debug.Log(paused);
        float timeElapsed = Time.time - startTime;
        ratio = (questionTime - timeElapsed) / questionTime;
        ratio = ratio >= 0.0f ? ratio : 0.0f;
        Vector3 position = Vector3.zero;
        position.z = ratio * 10 + 0.35f;

        // When the player has still not answered the question

        if (!paused)
        {

            timeBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
            timeBar.fillAmount = ratio;

            currAsteroid.transform.position = position;
            
            // If Time is up
            
            if (ratio <= 0.0f)
            {
                StoreScore(false, 0);

                //ResultManager.AddRecord(false, 0);
                PenalizePlayer();
                failedQuestion = false;
                BreakAsteroid();
                questionNumber++;
                paused = true;
                Invoke(nameof(DisplayQuestion), timeDelay);
            }
        }

        if (failedQuestion)
        {
            // If asteroid hit you
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                // Add has refinement
                PenalizePlayer();
                failedQuestion = false;
                BreakAsteroid();
                paused = true;
                Invoke(nameof(DisplayQuestion), timeDelay);
            }
        }
    }


    public void DisplayQuestion()
    {
        isLocked = false;

        if (IsGameOver())
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        failedQuestion = false;

        questionName.GetComponent<Text>().text = this.questionBank[questionNumber].questionName;
        this.questionBank[questionNumber].options = this.questionBank[questionNumber].options.OrderBy(x => Random.value).ToList();
        option1.GetComponent<Text>().text = this.questionBank[questionNumber].options[0].option;
        option2.GetComponent<Text>().text = this.questionBank[questionNumber].options[1].option;
        option3.GetComponent<Text>().text = this.questionBank[questionNumber].options[2].option;
        option4.GetComponent<Text>().text = this.questionBank[questionNumber].options[3].option;

        startTime = Time.time;
        currAsteroid = Instantiate(asteroid, new Vector3(0, 0, 15), Random.rotation);
        paused = false;
    }

    public void CheckAnswer(int optionNumber)
    {
        // If player has already selected an option
        if (isLocked)
            return;
        isLocked = true;

        List<Option> options = this.questionBank[questionNumber].options;

        bool result = false;

        // Answer is Correct 
        if (options[optionNumber].isCorrect)
        {
            result = true;
            RewardPlayer();
        }

        else
        {
            //PenalizePlayer();
            failedQuestion = true;
        }

        // Set Base Score to the questions score
        double baseScore = questionBank[questionNumber].points;
        double score = this.CalculateScore(result, baseScore);

        this.StoreScore(result, score);

        questionNumber += 1;
        paused = true;

    }

    void BreakAsteroid()
    {
        GameObject fractured = currAsteroid.GetComponent<Fracture>().returnFractured();
        fragment = Instantiate(fractured, currAsteroid.transform.position, currAsteroid.transform.rotation);
        Destroy(currAsteroid);
        Invoke(nameof(DestroyFragment), 1);
    }

    void DestroyFragment()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.stopMissile();
        Destroy(fragment);
    }

    public virtual void StoreScore(bool result, double score)
    {
        //ResultManager.AddRecord(result, score);
        AssignmentResultManager.AddRecord(result, score);
    }

    // Make Abstract
    public virtual double CalculateScore(bool result, double baseScore)
    {
        if (result)
        {
            return baseScore;
        }

        return 0.0;
    }

    public virtual void PenalizePlayer()
    {
        failedQuestion = true;
    }

    public virtual void RewardPlayer()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.releaseMissile();
        BreakAsteroid();
        Invoke(nameof(DisplayQuestion), timeDelay);
    }

    public virtual bool IsGameOver()
    {
        if (questionNumber < numQuestions)
            return false;

        return true;
    }


    // Initialize
    public virtual void initQstBank()
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
}
