using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ReceivedChallenge_single
{
    public int[] questionIds;
    public string _id;
    public string challengeName;
    public Receiver sender;
    public questionTopic[] questionTopics;
    public int myTime;
    public int myScore;
    public int myStatus;
}

[System.Serializable]
public class ReceivedChallenges
{
    public ReceivedChallenge_single[] received;
}

public class DeclinedChallengeInfo
{
    public string challengeID;
    public string emailID;
}


public class ReceivedChallengeDisplay : MonoBehaviour
{
    public string selectedChallengeid;
    public GameObject PendingChallenge;
    public GameObject CompletedChallenge;
    public GameObject infoContent;
    public GameObject scroll;
    public Canvas accept;
    public Canvas decline;
    public Canvas info;
    public bool accept_a = false;
    public bool decline_a = false;
    public bool info_a = false;
    public List<ReceivedChallenge_single> pending = new List<ReceivedChallenge_single>();
    public List<ReceivedChallenge_single> completed = new List<ReceivedChallenge_single>();
    public ReceivedChallenges ReceivedSets = new ReceivedChallenges();

    List<GameObject> challengeObjects = new List<GameObject>();

    // Start is called before the first frame update

    /*public static Dictionary<string, Dictionary<string, string>> RC_info =
 new Dictionary<string, Dictionary<string, string>>()
 {
        {
            "01",
            new Dictionary<string, string>
            {
                {"name", "The Ultimate Challenge"},
                {"sender", "Thanos"},
                {"attempted","0" },
                {"c_score","200" },
                {"c_time", "7 min"},
                {"your_score","0" },
                {"your_time","0" },
            }
        },
        {
            "02",
            new Dictionary<string, string>
            {
                {"name", "Awesome Challenge"},
                {"sender", "Hella"},
                {"c_score","150" },
                {"c_time", "5 min"},
                {"attempted","0" },
                {"your_score","0" },
                {"your_time","0" }
            }
        },
        {
            "03",
            new Dictionary<string, string>
            {
                {"name", "Infinity Stones"},
                {"sender", "Iron Man"},
                {"attempted","1" },
                {"c_score","100" },
                {"c_time", "5 min"},
                {"your_score","100" },
                {"your_time","2 min" }
            }
        },
        {
            "04",
            new Dictionary<string, string>
            {
                {"name", "Gauntlet"},
                {"sender", "Vision"},
                {"c_score","300" },
                {"c_time", "4 min"},
                {"attempted","1" },
                {"your_score","50" },
                {"your_time","1 min" }
            }
        },

 };

    Dictionary<string, List<string>> topic_info =
new Dictionary<string, List<string>>()
{
    { "01", new List<string>{"Requirements Engineering","Implementation"} },
    { "02", new List<string>{"SS1","SS2","SS3","SS4"} },
    { "03", new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
    { "04", new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
};*/

    void Awake()
    {
        // "emailID=aratrika001@e.ntu.edu.sg" use this for immediate testing
        Challenge.playerType = PlayerType.Challengee;

        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/challenge/getReceivedChallenges?emailID={0}", SecurityToken.Email),
        result =>
        {
            if (result != null)
            {
                Debug.Log(result);

                ReceivedChallenges ReceivedSet = JsonUtility.FromJson<ReceivedChallenges>("{ \"received\": " + result + "}");
                ReceivedSets = ReceivedSet;
                print(ReceivedSets.received.Length);
                Setup();

            }
            else
            {
                Debug.Log("No received challenges");

            }
        }
        ));



    }

    void Setup()
    {
       
        foreach (var entry in ReceivedSets.received)
        {
            if (entry.myStatus == 0)
            {
                pending.Add(entry);
            }
            else
            {
                completed.Add(entry);
            }
        }

        Vector3 curPos = new Vector3(430, -86, 0);
        int counter = 1;
        foreach (var item in pending)
        {
            
            var newPC = Instantiate(PendingChallenge, gameObject.transform, false);
            challengeObjects.Add(newPC);
            newPC.name = item._id;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "AcceptButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(accept_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
                else if (child.name == "DeclineButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(decline_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
                else if (child.name == "InfoButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(info_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                    btn.onClick.AddListener(setInfopopup);
                }
                else if(child.name=="SnoText")
                {
                    Text sText= child.GetComponentInChildren<Text>();
                    sText.text = counter.ToString();
                    counter += 1;
                }
                else if(child.name=="Cname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.challengeName;
                }

            }
            curPos -=new Vector3(0, 65, 0);

        }


        foreach (var item in completed)
        {

            var newPC = Instantiate(CompletedChallenge, gameObject.transform, false);
            challengeObjects.Add(newPC);
            newPC.name = item._id;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Score: " + item.myScore;
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Time: "+item.myTime;
                }
                else if (child.name == "InfoButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(info_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                    btn.onClick.AddListener(setInfopopup);

                }
                else if (child.name == "SnoText")
                {
                    Text sText = child.GetComponentInChildren<Text>();
                    sText.text = counter.ToString();
                    counter += 1;
                }
                else if (child.name == "Cname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.challengeName;
                }

            }
            curPos -= new Vector3(0, 65, 0);

        }


    }

    public void accept_pop()
    {
        if (accept_a == false)
        {
            accept_a = true;
            accept.enabled = true;
        }
        else if (accept_a == true)
        {
            accept_a = false;
            accept.enabled = false;
        }
    }

    public void decline_pop()
    {
        if (decline_a == false)
        {
            decline_a = true;
            decline.enabled = true;
        }
        else if (decline_a == true)
        {
            decline_a = false;
            decline.enabled = false;
        }
    }

    public void info_pop()
    {
        if (info_a == false)
        {
            info_a = true;
            info.enabled = true;
        }
        else if (info_a == true)
        {
            info_a = false;
            info.enabled = false;
        }
    }

    public void setSelectedID(Button btn)
    {
        Challenge.challengeID = btn.transform.parent.gameObject.name;
        Challenge.playerType = PlayerType.Challengee;
        Challenge.challengeName = btn.transform.parent.Find("Cname").GetComponent<Text>().text;
        selectedChallengeid = btn.transform.parent.gameObject.name;
    }

    public void setInfopopup()
    {
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;
        ReceivedChallenge_single selected = findSelectedChallenge(Challenge.challengeID);
        foreach (Transform child in infoContent.transform)
        {
             if (child.name == "Cname")
            {
                Text sText = child.GetComponentInChildren<Text>();
                string[] names = selected.sender.emailID.Split('@');
                sText.text = names[0];
            }
            else if (child.name == "CTime")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = selected.sender.timeTaken.ToString();
            }
            else if (child.name == "Cscore")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = selected.sender.score.ToString();
            }
            else if(child.name=="ScoreMessage")
            {
                Text cText = child.GetComponentInChildren<Text>();
                if(selected.myStatus==0)
                {
                    cText.text = "Can you beat this score?";
                }
                else if(selected.sender.score<= selected.myScore )
                {
                    cText.text = "Good Job!";
                }
                else
                {
                    cText.text = "Better luck next time";
                }
            }
            else if(child.name=="Topic List")
            {
                Text cText = child.GetComponentInChildren<Text>();
                string topics = "";
                List<string> uniNames = Multiverse.getUniverses();
                foreach(var i in selected.questionTopics)
                {
                    string topic = uniNames[i.universe] + "," + Multiverse.getSolarSystems(i.universe)[i.solarSystem];
                    topics += topic + "\n";
                }
                cText.text = topics;


            }
        }

    }

    public void OnDeclineYes()
    {
        bool start_shift = false;
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "ContentHeading")
            {
                continue;
            }
            else if (child.name==selectedChallengeid)
            {
                Destroy(child.gameObject);
                start_shift = true;
            }
            else if(start_shift)
            {
                child.localPosition += new Vector3(0, 65, 0);
                GameObject sno = child.transform.Find("SnoText").gameObject;
                Text sText = child.GetComponentInChildren<Text>();
                int s= int.Parse(sText.text);
                sText.text = (s - 1).ToString();
            }
        }


        DeclinedChallengeInfo declinedChallengeInfo = new DeclinedChallengeInfo();
        declinedChallengeInfo.challengeID = Challenge.challengeID;
        ReceivedChallenge_single selected = findSelectedChallenge(Challenge.challengeID);
        //for testing
        declinedChallengeInfo.emailID= SecurityToken.Email;
        string json = JsonUtility.ToJson(declinedChallengeInfo);

        print(json);
        StartCoroutine(ServerController.Put("http://localhost:5000/student/challenge/declineChallenge", json,
             result =>
             {
                 if (result != null)
                 {
                     //need to convert into complete object to store challenge id
                     Debug.Log(result);

                     /*for (int i = 0; i < questionResult.questions.Length; i++)
                     {
                         Debug.Log(questionResult.questions[i].body);
                     }*/
                 }
                 else
                 {
                     Debug.Log("No challengeID!");

                 }
             }
        ));

        //actual code
        // string emailID = SecurityToken.Email;




    }

    public void OnAcceptYes()
    {
        //add code to load new game scene and send challenge id to db
    }

    public ReceivedChallenge_single findSelectedChallenge(string selectID)
    {
        ReceivedChallenge_single selected = null;
        foreach (var item in ReceivedSets.received)
        {
            if (item._id == selectID)
            {
                selected = item;
            }
        }
        return selected;
    }

    public List<GameObject> GetGameObjects()
    {
        return challengeObjects;
    }
}
