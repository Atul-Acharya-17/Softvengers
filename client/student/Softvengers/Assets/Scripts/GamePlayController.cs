using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{/*
    public Navigation navigationData;

    List<List<Question>> questions = new List<List<Question>>();
    
    public Transform questionName;
    public Transform option1;
    public Transform option2;
    public Transform option3;
    public Transform option4;

    public Image healthBar;
    public Image timeBar;
    public GameObject asteroid;

    private GameObject fragment;
    private GameObject currAsteroid;

    private float timeDelay = 1.25f;
    private float health = 100.0f;
    private float maxHealth = 100.0f;

    private float startTime = 0.0f;
    private float questionTime = 7f;
    private float ratio;
    private float baseScore = 10;

    private bool paused = false;

    private bool failedQuestion = false;
    private bool isLocked = false;

    private int incorrectStreak = 0;
    private int correctStreak = 0;
    private int correctThreshold = 6;
    private int incorrectThreshold = 3;
    private int questionNumber = 0;
    private int questDifficulty;
    private int planetDifficulty;

    List<Question> questionsE = new List<Question>();
    List<Question> questionsM = new List<Question>();
    List<Question> questionsH = new List<Question>();
    private int questionNumberE = 0;
    private int questionNumberM = 0;
    private int questionNumberH = 0;
    
    public GamePlayController()
    { 
        initEasyQsts();
        initMedQsts();
        initHardQsts();
        this.questions.Add(questionsE);
        this.questions.Add(questionsM);
        this.questions.Add(questionsH);

        

        Debug.Log(planetDifficulty);
    }

    
    public void initEasyQsts()
    {
        questionNumberE = 0;

        Option o1 = new Option("A", true);
        Option o2 = new Option("B", false);
        Option o3 = new Option("C", false);
        Option o4 = new Option("D", false);
        List<Option> ol1 = new List<Option>();
        ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        Question q1 = new Question("1E", ol1);

        Option o5 = new Option("E", false);
        Option o6 = new Option("F", true);
        Option o7 = new Option("G", false);
        Option o8 = new Option("H", false);
        List<Option> ol2 = new List<Option>();
        ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
        Question q2 = new Question("2E", ol2);

        Option o9 = new Option("I", false);
        Option o10 = new Option("J", false);
        Option o11 = new Option("K", true);
        Option o12 = new Option("L", false);
        List<Option> ol3 = new List<Option>();
        ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
        Question q3 = new Question("3E", ol3);

        Option o13 = new Option("M", false);
        Option o14 = new Option("N", false);
        Option o15 = new Option("O", false);
        Option o16 = new Option("P", true);
        List<Option> ol4 = new List<Option>();
        ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
        Question q4 = new Question("4E", ol4);

        Option o17 = new Option("Q", false);
        Option o18 = new Option("R", false);
        Option o19 = new Option("S", false);
        Option o20 = new Option("T", true);
        List<Option> ol5 = new List<Option>();
        ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
        Question q5 = new Question("5E", ol5);

        Option o21 = new Option("A", true);
        Option o22 = new Option("B", false);
        Option o23 = new Option("C", false);
        Option o24 = new Option("D", false);
        List<Option> ol6 = new List<Option>();
        ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
        Question q6 = new Question("6E", ol6);

        Option o25 = new Option("E", false);
        Option o26 = new Option("F", true);
        Option o27 = new Option("G", false);
        Option o28 = new Option("H", false);
        List<Option> ol7 = new List<Option>();
        ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
        Question q7 = new Question("7E", ol7);

        Option o29 = new Option("I", false);
        Option o30 = new Option("J", false);
        Option o31 = new Option("K", true);
        Option o32 = new Option("L", false);
        List<Option> ol8 = new List<Option>();
        ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
        Question q8 = new Question("8E", ol8);

        Option o33 = new Option("M", false);
        Option o34 = new Option("N", false);
        Option o35 = new Option("O", false);
        Option o36 = new Option("P", true);
        List<Option> ol9 = new List<Option>();
        ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
        Question q9 = new Question("9E", ol9);

        Option o37 = new Option("Q", false);
        Option o38 = new Option("R", false);
        Option o39 = new Option("S", false);
        Option o40 = new Option("T", true);
        List<Option> ol10 = new List<Option>();
        ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
        Question q10 = new Question("10E", ol10);

        this.questionsE.Add(q1);
        this.questionsE.Add(q2);
        this.questionsE.Add(q3);
        this.questionsE.Add(q4);
        this.questionsE.Add(q5);
        this.questionsE.Add(q6);
        this.questionsE.Add(q7);
        this.questionsE.Add(q8);
        this.questionsE.Add(q9);
        this.questionsE.Add(q10);
    }

    void initMedQsts()
    {
        questionNumberM = 0;

        Option o1 = new Option("A", true);
        Option o2 = new Option("B", false);
        Option o3 = new Option("C", false);
        Option o4 = new Option("D", false);
        List<Option> ol1 = new List<Option>();
        ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        Question q1 = new Question("1M", ol1);

        Option o5 = new Option("E", false);
        Option o6 = new Option("F", true);
        Option o7 = new Option("G", false);
        Option o8 = new Option("H", false);
        List<Option> ol2 = new List<Option>();
        ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
        Question q2 = new Question("2M", ol2);

        Option o9 = new Option("I", false);
        Option o10 = new Option("J", false);
        Option o11 = new Option("K", true);
        Option o12 = new Option("L", false);
        List<Option> ol3 = new List<Option>();
        ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
        Question q3 = new Question("3M", ol3);

        Option o13 = new Option("M", false);
        Option o14 = new Option("N", false);
        Option o15 = new Option("O", false);
        Option o16 = new Option("P", true);
        List<Option> ol4 = new List<Option>();
        ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
        Question q4 = new Question("4M", ol4);

        Option o17 = new Option("Q", false);
        Option o18 = new Option("R", false);
        Option o19 = new Option("S", false);
        Option o20 = new Option("T", true);
        List<Option> ol5 = new List<Option>();
        ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
        Question q5 = new Question("5M", ol5);

        Option o21 = new Option("A", true);
        Option o22 = new Option("B", false);
        Option o23 = new Option("C", false);
        Option o24 = new Option("D", false);
        List<Option> ol6 = new List<Option>();
        ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
        Question q6 = new Question("6M", ol6);

        Option o25 = new Option("E", false);
        Option o26 = new Option("F", true);
        Option o27 = new Option("G", false);
        Option o28 = new Option("H", false);
        List<Option> ol7 = new List<Option>();
        ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
        Question q7 = new Question("7M", ol7);

        Option o29 = new Option("I", false);
        Option o30 = new Option("J", false);
        Option o31 = new Option("K", true);
        Option o32 = new Option("L", false);
        List<Option> ol8 = new List<Option>();
        ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
        Question q8 = new Question("8M", ol8);

        Option o33 = new Option("M", false);
        Option o34 = new Option("N", false);
        Option o35 = new Option("O", false);
        Option o36 = new Option("P", true);
        List<Option> ol9 = new List<Option>();
        ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
        Question q9 = new Question("9M", ol9);

        Option o37 = new Option("Q", false);
        Option o38 = new Option("R", false);
        Option o39 = new Option("S", false);
        Option o40 = new Option("T", true);
        List<Option> ol10 = new List<Option>();
        ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
        Question q10 = new Question("10M", ol10);

        this.questionsM.Add(q1);
        this.questionsM.Add(q2);
        this.questionsM.Add(q3);
        this.questionsM.Add(q4);
        this.questionsM.Add(q5);
        this.questionsM.Add(q6);
        this.questionsM.Add(q7);
        this.questionsM.Add(q8);
        this.questionsM.Add(q9);
        this.questionsM.Add(q10);
    }

    void initHardQsts()
    {
        questionNumberH = 0;

        Option o1 = new Option("A", true);
        Option o2 = new Option("B", false);
        Option o3 = new Option("C", false);
        Option o4 = new Option("D", false);
        List<Option> ol1 = new List<Option>();
        ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        Question q1 = new Question("1H", ol1);

        Option o5 = new Option("E", false);
        Option o6 = new Option("F", true);
        Option o7 = new Option("G", false);
        Option o8 = new Option("H", false);
        List<Option> ol2 = new List<Option>();
        ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
        Question q2 = new Question("2H", ol2);

        Option o9 = new Option("I", false);
        Option o10 = new Option("J", false);
        Option o11 = new Option("K", true);
        Option o12 = new Option("L", false);
        List<Option> ol3 = new List<Option>();
        ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
        Question q3 = new Question("3H", ol3);

        Option o13 = new Option("M", false);
        Option o14 = new Option("N", false);
        Option o15 = new Option("O", false);
        Option o16 = new Option("P", true);
        List<Option> ol4 = new List<Option>();
        ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
        Question q4 = new Question("4H", ol4);

        Option o17 = new Option("Q", false);
        Option o18 = new Option("R", false);
        Option o19 = new Option("S", false);
        Option o20 = new Option("T", true);
        List<Option> ol5 = new List<Option>();
        ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
        Question q5 = new Question("5H", ol5);

        Option o21 = new Option("A", true);
        Option o22 = new Option("B", false);
        Option o23 = new Option("C", false);
        Option o24 = new Option("D", false);
        List<Option> ol6 = new List<Option>();
        ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
        Question q6 = new Question("6H", ol6);

        Option o25 = new Option("E", false);
        Option o26 = new Option("F", true);
        Option o27 = new Option("G", false);
        Option o28 = new Option("H", false);
        List<Option> ol7 = new List<Option>();
        ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
        Question q7 = new Question("7H", ol7);

        Option o29 = new Option("I", false);
        Option o30 = new Option("J", false);
        Option o31 = new Option("K", true);
        Option o32 = new Option("L", false);
        List<Option> ol8 = new List<Option>();
        ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
        Question q8 = new Question("8H", ol8);

        Option o33 = new Option("M", false);
        Option o34 = new Option("N", false);
        Option o35 = new Option("O", false);
        Option o36 = new Option("P", true);
        List<Option> ol9 = new List<Option>();
        ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
        Question q9 = new Question("9H", ol9);

        Option o37 = new Option("Q", false);
        Option o38 = new Option("R", false);
        Option o39 = new Option("S", false);
        Option o40 = new Option("T", true);
        List<Option> ol10 = new List<Option>();
        ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
        Question q10 = new Question("10H", ol10);

        this.questionsH.Add(q1);
        this.questionsH.Add(q2);
        this.questionsH.Add(q3);
        this.questionsH.Add(q4);
        this.questionsH.Add(q5);
        this.questionsH.Add(q6);
        this.questionsH.Add(q7);
        this.questionsH.Add(q8);
        this.questionsH.Add(q9);
        this.questionsH.Add(q10);
    }


    void Start()
    {
        planetDifficulty = navigationData.planetSelected;
        questDifficulty = planetDifficulty;
        paused = true;
        DisplayQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        ratio = (questionTime - timeElapsed) / questionTime;
        ratio = ratio >= 0.0f ? ratio : 0.0f;
        Vector3 position = Vector3.zero;
        position.z = ratio * 10 + 0.35f;
        if (!paused) {

            timeBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
            timeBar.fillAmount = ratio;
            
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                DecreaseHealth();
                ResultManager.AddRecord(false, 0);
                BreakAsteroid();
                questionNumber++;
                paused = true;

                if (questDifficulty == planetDifficulty)
                {
                    incorrectStreak++;
                    correctStreak = 0;
                    if (incorrectStreak == incorrectThreshold && questDifficulty > 0)
                    {
                        questDifficulty--;
                    }
                }
                else if (questDifficulty > planetDifficulty)
                {
                    correctStreak--;
                    questDifficulty--;
                }

                Invoke("DisplayQuestion", timeDelay); 
            }
        }

        if (failedQuestion)
        {
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                DecreaseHealth();
                BreakAsteroid();
                paused = true;
                failedQuestion = false;
                Invoke("DisplayQuestion", timeDelay);
            }
        }
    }

    void DisplayQuestion()
    {
    
        isLocked = false;
        if (questionNumber == questions[questDifficulty].Count || GameOver())
        {
            SceneManager.LoadScene("ResultScene");
            return;
        }

        failedQuestion = false;
        questionName.GetComponent<Text>().text = this.questions[questDifficulty][questionNumber].questionName;
        option1.GetComponent<Text>().text = this.questions[questDifficulty][questionNumber].options[0].option;
        option2.GetComponent<Text>().text = this.questions[questDifficulty][questionNumber].options[1].option;
        option3.GetComponent<Text>().text = this.questions[questDifficulty][questionNumber].options[2].option;
        option4.GetComponent<Text>().text = this.questions[questDifficulty][questionNumber].options[3].option;
        startTime = Time.time;
        currAsteroid = Instantiate(asteroid, new Vector3(0, 0, 15), Random.rotation);
        paused = false;
    }

    public void CheckAnswer(int optionNumber)
    {
        if (isLocked)
            return;
        isLocked = true;
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        List<Option> options = this.questions[questDifficulty][questionNumber].options;

        if (options[optionNumber].isCorrect == true)
        {
            if (questDifficulty == planetDifficulty)
            {
                correctStreak++;
                incorrectStreak = 0;
                if (correctStreak == correctThreshold && questDifficulty < 2)
                {
                    questDifficulty++;
                }
            }
            else if (questDifficulty < planetDifficulty)
            {
                incorrectStreak--;
                questDifficulty++;
            }
            float score = baseScore + ratio * baseScore;
            ResultManager.AddRecord(true, score);
            Debug.Log("Correct");
            
            shooter.releaseMissile();
            BreakAsteroid();
            questionNumber++;
            Invoke("DisplayQuestion", timeDelay);
        }
        else
        {
            if (questDifficulty == planetDifficulty)
            {
                incorrectStreak++;
                correctStreak = 0;
                if (incorrectStreak == incorrectThreshold && questDifficulty > 0)
                {
                    questDifficulty--;
                }
            }
            else if (questDifficulty > planetDifficulty)
            {
                correctStreak--;
                questDifficulty--;
            }
            failedQuestion = true;
            Debug.Log("Wrong");
            float score = 0.0f;
            ResultManager.AddRecord(false, score);
           

            questionNumber++;
            
        }
        paused = true;
    }



    void BreakAsteroid()
    {
        GameObject fractured = currAsteroid.GetComponent<Fracture>().returnFractured();
        fragment = Instantiate(fractured, currAsteroid.transform.position, currAsteroid.transform.rotation);
        Destroy(currAsteroid);
        Invoke("DestroyFragment", 1);
    }

    void DestroyFragment()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.stopMissile();
        Destroy(fragment);
    }

    void DecreaseHealth()
    {
        health -= 20.0f;
        float ratio = health / maxHealth;
        healthBar.fillAmount = ratio;
        healthBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
    }

    public virtual bool GameOver()
    {
        if (health <= 0.0f)
            return true;
        return false;
    }
    */
}

