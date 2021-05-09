using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class qsts
{
    public string[] wrongOptions;
    public int questionID;
    public int solarID;
    public int universeID;
    public int planetID;
    public string body;
    public string correctOption;
    public int points;
    public int __v;
}

[System.Serializable]
public class QuestionResult
{
    public qsts[] easy;
    public qsts[] medium;
    public qsts[] hard;
}

public class SoloGamePlayController : ChallengeGameController
{
    public Navigation navigationData;

    public Image healthBar;
    private float maxHealth = 100.0f;
    private float health = 100.0f;

    // For Adaptive Questioning 
    private int questDifficulty;
    private int planetDifficulty;

    private int incorrectStreak = 0;
    private int correctStreak = 0;
    private int correctThreshold = 6;
    private int incorrectThreshold = 3;

    //private List<Question> questionBank;
    private QuestionResult questionResult;

    List<Question> questionsE = new List<Question>();
    List<Question> questionsM = new List<Question>();
    List<Question> questionsH = new List<Question>();

    private List<List<Question>> questions = new List<List<Question>>();

    protected override void Start()
    {
        paused = true;
        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/questions/?universe={0}&solarSystem={1}", navigationData.universeSelected, navigationData.solarSystemSelected),
        result =>
        {
            if (result != null)
            {
                Debug.Log(result);
                questionResult = JsonUtility.FromJson<QuestionResult>(result);

                initEasyQsts();
                initMedQsts();
                initHardQsts();

                this.questions.Add(questionsE);
                this.questions.Add(questionsM);
                this.questions.Add(questionsH);
                Debug.Log("Num:" + this.questions.Count);

                for (int i = 0; i < questionResult.easy.Length; i++)
                {
                    Debug.Log(questionResult.easy[i].body);
                }

                planetDifficulty = navigationData.planetSelected;
                questDifficulty = planetDifficulty;
                questionBank = questions[questDifficulty];
                
                DisplayQuestion();
            }
            else
            {
                Debug.Log("No questions!");

            }
        }
        ));


    }

    

    public override void StoreScore(bool result, double score)
    {
        ResultManager.AddRecord(result, score);
    }

    public override void RewardPlayer()
    {
        base.RewardPlayer();

        /*
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
        */
        questDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnCorrect(planetDifficulty, questDifficulty, ref correctStreak, ref incorrectStreak);

        // Change Question Bank
        questionBank = questions[questDifficulty];
    }

    public override void PenalizePlayer()
    {
        base.PenalizePlayer();
        DecreaseHealth();
        /*
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
        */
        questDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnWrong(planetDifficulty, questDifficulty, ref correctStreak, ref incorrectStreak);

        // Change Question Bank
        questionBank = questions[questDifficulty];
    }

    public void DecreaseHealth()
    {
        health -= 20.0f;
        float ratio = health / maxHealth;
        healthBar.fillAmount = ratio;
        healthBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
    }


    public override bool IsGameOver()
    {
        if (questionNumber < numQuestions && health > 0.0)
            return false;
        return true;
    }


    public SoloGamePlayController()
    {
        this.nextScene = "ResultScene";
        numQuestions = 10;
        
        Debug.Log(planetDifficulty);
    }






    // Initialization

    public void initEasyQsts()
    {

        for (int i=0; i<questionResult.easy.Length; i++)
        {
            string body = questionResult.easy[i].body;
            int points = questionResult.easy[i].points;
            List<Option> ol = new List<Option>();
            Option corr = new Option(questionResult.easy[i].correctOption, true);
            ol.Add(corr);
            for (int j=0; j<3; j++)
            {
                Option incorr = new Option(questionResult.easy[i].wrongOptions[j], false);
                ol.Add(incorr);
            }
            Question q = new Question(body, ol, points);
            this.questionsE.Add(q);
        }

        //Option o1 = new Option("A", true);
        //Option o2 = new Option("B", false);
        //Option o3 = new Option("C", false);
        //Option o4 = new Option("D", false);
        //List<Option> ol1 = new List<Option>();
        //ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        //Question q1 = new Question("1E", ol1);
        //this.questionsE.Add(q1);
        
    }

    void initMedQsts()
    {
        for (int i = 0; i < questionResult.medium.Length; i++)
        {
            string body = questionResult.medium[i].body;
            int points = questionResult.medium[i].points;
            List<Option> ol = new List<Option>();
            Option corr = new Option(questionResult.medium[i].correctOption, true);
            ol.Add(corr);
            for (int j = 0; j < 3; j++)
            {
                Option incorr = new Option(questionResult.medium[i].wrongOptions[j], false);
                ol.Add(incorr);
            }
            Question q = new Question(body, ol, points);
            this.questionsM.Add(q);
        }

    }

    void initHardQsts()
    {
        for (int i = 0; i < questionResult.hard.Length; i++)
        {
            string body = questionResult.hard[i].body;
            int points = questionResult.hard[i].points;
            List<Option> ol = new List<Option>();
            Option corr = new Option(questionResult.hard[i].correctOption, true);
            ol.Add(corr);
            for (int j = 0; j < 3; j++)
            {
                Option incorr = new Option(questionResult.hard[i].wrongOptions[j], false);
                ol.Add(incorr);
            }
            Question q = new Question(body, ol, points);
            this.questionsH.Add(q);
        }
    }

    //public void initEasyQsts()
    //{

    //    Option o1 = new Option("A", true);
    //    Option o2 = new Option("B", false);
    //    Option o3 = new Option("C", false);
    //    Option o4 = new Option("D", false);
    //    List<Option> ol1 = new List<Option>();
    //    ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
    //    Question q1 = new Question("1E", ol1);

    //    Option o5 = new Option("E", false);
    //    Option o6 = new Option("F", true);
    //    Option o7 = new Option("G", false);
    //    Option o8 = new Option("H", false);
    //    List<Option> ol2 = new List<Option>();
    //    ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
    //    Question q2 = new Question("2E", ol2);

    //    Option o9 = new Option("I", false);
    //    Option o10 = new Option("J", false);
    //    Option o11 = new Option("K", true);
    //    Option o12 = new Option("L", false);
    //    List<Option> ol3 = new List<Option>();
    //    ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
    //    Question q3 = new Question("3E", ol3);

    //    Option o13 = new Option("M", false);
    //    Option o14 = new Option("N", false);
    //    Option o15 = new Option("O", false);
    //    Option o16 = new Option("P", true);
    //    List<Option> ol4 = new List<Option>();
    //    ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
    //    Question q4 = new Question("4E", ol4);

    //    Option o17 = new Option("Q", false);
    //    Option o18 = new Option("R", false);
    //    Option o19 = new Option("S", false);
    //    Option o20 = new Option("T", true);
    //    List<Option> ol5 = new List<Option>();
    //    ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
    //    Question q5 = new Question("5E", ol5);

    //    Option o21 = new Option("A", true);
    //    Option o22 = new Option("B", false);
    //    Option o23 = new Option("C", false);
    //    Option o24 = new Option("D", false);
    //    List<Option> ol6 = new List<Option>();
    //    ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
    //    Question q6 = new Question("6E", ol6);

    //    Option o25 = new Option("E", false);
    //    Option o26 = new Option("F", true);
    //    Option o27 = new Option("G", false);
    //    Option o28 = new Option("H", false);
    //    List<Option> ol7 = new List<Option>();
    //    ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
    //    Question q7 = new Question("7E", ol7);

    //    Option o29 = new Option("I", false);
    //    Option o30 = new Option("J", false);
    //    Option o31 = new Option("K", true);
    //    Option o32 = new Option("L", false);
    //    List<Option> ol8 = new List<Option>();
    //    ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
    //    Question q8 = new Question("8E", ol8);

    //    Option o33 = new Option("M", false);
    //    Option o34 = new Option("N", false);
    //    Option o35 = new Option("O", false);
    //    Option o36 = new Option("P", true);
    //    List<Option> ol9 = new List<Option>();
    //    ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
    //    Question q9 = new Question("9E", ol9);

    //    Option o37 = new Option("Q", false);
    //    Option o38 = new Option("R", false);
    //    Option o39 = new Option("S", false);
    //    Option o40 = new Option("T", true);
    //    List<Option> ol10 = new List<Option>();
    //    ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
    //    Question q10 = new Question("10E", ol10);

    //    this.questionsE.Add(q1);
    //    this.questionsE.Add(q2);
    //    this.questionsE.Add(q3);
    //    this.questionsE.Add(q4);
    //    this.questionsE.Add(q5);
    //    this.questionsE.Add(q6);
    //    this.questionsE.Add(q7);
    //    this.questionsE.Add(q8);
    //    this.questionsE.Add(q9);
    //    this.questionsE.Add(q10);
    //}

    //void initMedQsts()
    //{

    //    Option o1 = new Option("A", true);
    //    Option o2 = new Option("B", false);
    //    Option o3 = new Option("C", false);
    //    Option o4 = new Option("D", false);
    //    List<Option> ol1 = new List<Option>();
    //    ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
    //    Question q1 = new Question("1M", ol1);

    //    Option o5 = new Option("E", false);
    //    Option o6 = new Option("F", true);
    //    Option o7 = new Option("G", false);
    //    Option o8 = new Option("H", false);
    //    List<Option> ol2 = new List<Option>();
    //    ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
    //    Question q2 = new Question("2M", ol2);

    //    Option o9 = new Option("I", false);
    //    Option o10 = new Option("J", false);
    //    Option o11 = new Option("K", true);
    //    Option o12 = new Option("L", false);
    //    List<Option> ol3 = new List<Option>();
    //    ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
    //    Question q3 = new Question("3M", ol3);

    //    Option o13 = new Option("M", false);
    //    Option o14 = new Option("N", false);
    //    Option o15 = new Option("O", false);
    //    Option o16 = new Option("P", true);
    //    List<Option> ol4 = new List<Option>();
    //    ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
    //    Question q4 = new Question("4M", ol4);

    //    Option o17 = new Option("Q", false);
    //    Option o18 = new Option("R", false);
    //    Option o19 = new Option("S", false);
    //    Option o20 = new Option("T", true);
    //    List<Option> ol5 = new List<Option>();
    //    ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
    //    Question q5 = new Question("5M", ol5);

    //    Option o21 = new Option("A", true);
    //    Option o22 = new Option("B", false);
    //    Option o23 = new Option("C", false);
    //    Option o24 = new Option("D", false);
    //    List<Option> ol6 = new List<Option>();
    //    ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
    //    Question q6 = new Question("6M", ol6);

    //    Option o25 = new Option("E", false);
    //    Option o26 = new Option("F", true);
    //    Option o27 = new Option("G", false);
    //    Option o28 = new Option("H", false);
    //    List<Option> ol7 = new List<Option>();
    //    ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
    //    Question q7 = new Question("7M", ol7);

    //    Option o29 = new Option("I", false);
    //    Option o30 = new Option("J", false);
    //    Option o31 = new Option("K", true);
    //    Option o32 = new Option("L", false);
    //    List<Option> ol8 = new List<Option>();
    //    ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
    //    Question q8 = new Question("8M", ol8);

    //    Option o33 = new Option("M", false);
    //    Option o34 = new Option("N", false);
    //    Option o35 = new Option("O", false);
    //    Option o36 = new Option("P", true);
    //    List<Option> ol9 = new List<Option>();
    //    ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
    //    Question q9 = new Question("9M", ol9);

    //    Option o37 = new Option("Q", false);
    //    Option o38 = new Option("R", false);
    //    Option o39 = new Option("S", false);
    //    Option o40 = new Option("T", true);
    //    List<Option> ol10 = new List<Option>();
    //    ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
    //    Question q10 = new Question("10M", ol10);

    //    this.questionsM.Add(q1);
    //    this.questionsM.Add(q2);
    //    this.questionsM.Add(q3);
    //    this.questionsM.Add(q4);
    //    this.questionsM.Add(q5);
    //    this.questionsM.Add(q6);
    //    this.questionsM.Add(q7);
    //    this.questionsM.Add(q8);
    //    this.questionsM.Add(q9);
    //    this.questionsM.Add(q10);
    //}

    //void initHardQsts()
    //{

    //    Option o1 = new Option("A", true);
    //    Option o2 = new Option("B", false);
    //    Option o3 = new Option("C", false);
    //    Option o4 = new Option("D", false);
    //    List<Option> ol1 = new List<Option>();
    //    ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
    //    Question q1 = new Question("1H", ol1);

    //    Option o5 = new Option("E", false);
    //    Option o6 = new Option("F", true);
    //    Option o7 = new Option("G", false);
    //    Option o8 = new Option("H", false);
    //    List<Option> ol2 = new List<Option>();
    //    ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
    //    Question q2 = new Question("2H", ol2);

    //    Option o9 = new Option("I", false);
    //    Option o10 = new Option("J", false);
    //    Option o11 = new Option("K", true);
    //    Option o12 = new Option("L", false);
    //    List<Option> ol3 = new List<Option>();
    //    ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
    //    Question q3 = new Question("3H", ol3);

    //    Option o13 = new Option("M", false);
    //    Option o14 = new Option("N", false);
    //    Option o15 = new Option("O", false);
    //    Option o16 = new Option("P", true);
    //    List<Option> ol4 = new List<Option>();
    //    ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
    //    Question q4 = new Question("4H", ol4);

    //    Option o17 = new Option("Q", false);
    //    Option o18 = new Option("R", false);
    //    Option o19 = new Option("S", false);
    //    Option o20 = new Option("T", true);
    //    List<Option> ol5 = new List<Option>();
    //    ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
    //    Question q5 = new Question("5H", ol5);

    //    Option o21 = new Option("A", true);
    //    Option o22 = new Option("B", false);
    //    Option o23 = new Option("C", false);
    //    Option o24 = new Option("D", false);
    //    List<Option> ol6 = new List<Option>();
    //    ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
    //    Question q6 = new Question("6H", ol6);

    //    Option o25 = new Option("E", false);
    //    Option o26 = new Option("F", true);
    //    Option o27 = new Option("G", false);
    //    Option o28 = new Option("H", false);
    //    List<Option> ol7 = new List<Option>();
    //    ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
    //    Question q7 = new Question("7H", ol7);

    //    Option o29 = new Option("I", false);
    //    Option o30 = new Option("J", false);
    //    Option o31 = new Option("K", true);
    //    Option o32 = new Option("L", false);
    //    List<Option> ol8 = new List<Option>();
    //    ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
    //    Question q8 = new Question("8H", ol8);

    //    Option o33 = new Option("M", false);
    //    Option o34 = new Option("N", false);
    //    Option o35 = new Option("O", false);
    //    Option o36 = new Option("P", true);
    //    List<Option> ol9 = new List<Option>();
    //    ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
    //    Question q9 = new Question("9H", ol9);

    //    Option o37 = new Option("Q", false);
    //    Option o38 = new Option("R", false);
    //    Option o39 = new Option("S", false);
    //    Option o40 = new Option("T", true);
    //    List<Option> ol10 = new List<Option>();
    //    ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
    //    Question q10 = new Question("10H", ol10);

    //    this.questionsH.Add(q1);
    //    this.questionsH.Add(q2);
    //    this.questionsH.Add(q3);
    //    this.questionsH.Add(q4);
    //    this.questionsH.Add(q5);
    //    this.questionsH.Add(q6);
    //    this.questionsH.Add(q7);
    //    this.questionsH.Add(q8);
    //    this.questionsH.Add(q9);
    //    this.questionsH.Add(q10);
    //}
}
