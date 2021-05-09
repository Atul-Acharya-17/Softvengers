using System.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class AssignmentList
{
    public List<Assignment> assignments;
}

[System.Serializable]
public class Assignment
{
    public string _id;
    public int assignmentID;
    public string assignmentName;
    public int timeLimit;
    public string deadline;
    public string tutGrp;
    public float myScore;
    public bool myStatus;

}


public class AssignmentScene : MonoBehaviour
{
    public static int selectedAssignmentid;
    public GameObject PendingAssignment;
    public GameObject CompletedAssignment;
    public GameObject OverdueAssignment;
    // public GameObject infoContent;
    // public GameObject scroll;
    public Canvas start;
    public Canvas completed;
    public Canvas overdue;
    public bool start_a = false;
    public bool completed_a = false;
    public bool overdue_a = false;
    public List<Assignment> pending_assignment = new List<Assignment>();
    public List<Assignment> completed_assignment = new List<Assignment>();
    public List<Assignment> overdue_assignment = new List<Assignment>();
    public AssignmentList AssignmentSets=new AssignmentList();

    List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update

    public static Dictionary<string, Dictionary<string, string>> Assign_info =
 new Dictionary<string, Dictionary<string, string>>()
 {
        {
            "01",
            new Dictionary<string, string>
            {
                {"name", "The Ultimate Assignment"},
                {"attempted","1" },
                {"c_score","200" },
                {"c_time", "7 min"},
                {"deadline","25/03/2021" }
            }
        },
        {
            "02",
            new Dictionary<string, string>
            {
                {"name", "Awesome Challenge"},
                {"attempted","0" },
                {"c_score","0" },
                {"c_time", "0"},
                {"deadline","24/03/2021" }
            }
        },
        {
            "03",
            new Dictionary<string, string>
            {
                {"name", "Infinity Stones"},
                {"attempted","0" },
                {"c_score","0" },
                {"c_time", "0"},
                {"deadline","27/03/2021" }
            }
        },
       

 };

    void Awake()
    {
    pending_assignment = new List<Assignment>();
    completed_assignment = new List<Assignment>();
    overdue_assignment = new List<Assignment>();
    AssignmentSets = new AssignmentList();
    //SecurityToken.MatricNo
    StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/assignments/getassignmentList?matricNo={0}", SecurityToken.MatricNo),
            result =>
            {
                if (result != null)
                {
                    Debug.Log("Received");
                    Debug.Log(result);
                    AssignmentList assignmentList = JsonUtility.FromJson<AssignmentList>("{\"assignments\":" + result + "}");
                    Debug.Log(assignmentList.assignments.Count);
                    AssignmentSets = assignmentList;
                    Setup();
                }
                else
                {
                    Debug.Log("Empty");
                }
            }
            ));

    }


    void Setup()
    {         
        foreach (var entry in AssignmentSets.assignments)
        {
            if (entry.myStatus == false)
            {
                DateTime localdate = DateTime.Now;
                CultureInfo provider = CultureInfo.InvariantCulture;
                print("Testing");
                print(entry.deadline);
                string[] timings = entry.deadline.Split('T');
                print(timings[0]);
                DateTime assign_deadline = DateTime.ParseExact(timings[0], "yyyy-MM-dd", provider);
                print(entry.deadline);
                entry.deadline = timings[0];
                Debug.Log(localdate);
                Debug.Log(assign_deadline);
                Debug.Log(DateTime.Compare(localdate, assign_deadline));
                if (DateTime.Compare(localdate, assign_deadline) > 0)
                    overdue_assignment.Add(entry);
                else
                    pending_assignment.Add(entry);

            }
            else
            {
                string[] timings = entry.deadline.Split('T');
                entry.deadline = timings[0];
                completed_assignment.Add(entry);
            }
        }

        int counter = 1;
        Vector3 curPos = new Vector3(480, -86, 0);
        foreach (var item in pending_assignment)
        {

            var newPA = Instantiate(PendingAssignment, gameObject.transform, false);
            gameObjects.Add(newPA);
            newPA.name = item.assignmentID.ToString();   // what is assignment id and _id??
            newPA.transform.localPosition = curPos;
            foreach (Transform child in newPA.transform)
            {
                if (child.name == "StartButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(start_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
               
                else if (child.name == "SnoText")
                {
                    Text sText = child.GetComponentInChildren<Text>();
                    sText.text = counter.ToString();
                    counter += 1;
                }
                else if (child.name == "Aname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.assignmentName;
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.timeLimit.ToString();
                }
                else if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.myScore.ToString();
                }
                else if (child.name == "Deadline")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.deadline;
                }

            }
            curPos -= new Vector3(0, 65, 0);

        }

        foreach (var item in overdue_assignment)
        {

            var newPA = Instantiate(OverdueAssignment, gameObject.transform, false);
            gameObjects.Add(newPA);
            newPA.name = item.assignmentID.ToString();
            newPA.transform.localPosition = curPos;
            foreach (Transform child in newPA.transform)
            {
               
                if (child.name == "OverdueButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(overdue_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                    
                }
                else if (child.name == "SnoText")
                {
                    Text sText = child.GetComponentInChildren<Text>();
                    sText.text = counter.ToString();
                    counter += 1;
                }
                else if (child.name == "Aname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.assignmentName;
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.timeLimit.ToString();
                }
                else if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.myScore.ToString();
                }
                else if (child.name == "Deadline")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.deadline;
                }

            }
            curPos -= new Vector3(0, 65, 0);

        }

        foreach (var item in completed_assignment)
        {

            var newPA = Instantiate(CompletedAssignment, gameObject.transform, false);
            gameObjects.Add(newPA);
            newPA.name = item.assignmentName;
            newPA.transform.localPosition = curPos;
            
            foreach (Transform child in newPA.transform)
            {
                if (child.name == "CompletedButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(completed_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
                
                else if (child.name == "SnoText")
                {
                    Text sText = child.GetComponentInChildren<Text>();
                    sText.text = counter.ToString();
                    counter += 1;
                }
                else if (child.name == "Aname")
                {
                    Text cText = child.GetComponent<Text>();
                    cText.text = item.assignmentName;
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.timeLimit.ToString();
                }
                else if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.myScore.ToString();
                }
                else if (child.name == "Deadline")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.deadline;
                }

            }
            curPos -= new Vector3(0, 65, 0);

        }

        



    }

    public void start_pop()
    {
        if (start_a == false)
        {
            start_a = true;
            start.enabled = true;
            completed_a = false;
            completed.enabled = false;
            overdue_a = false;
            overdue.enabled = false;
        }
        else if (start_a == true)
        {
            start_a = false;
            start.enabled = false;
        }
    }

    public void completed_pop()
    {
        if (completed_a == false)
        {
            completed_a = true;
            completed.enabled = true;
            start_a = false;
            start.enabled = false;
            overdue_a = false;
            overdue.enabled = false;
        }
        else if (completed_a == true)
        {
            completed_a = false;
            completed.enabled = false;
        }
    }

    public void overdue_pop()
    {
        if (overdue_a == false)
        {
            overdue_a = true;
            overdue.enabled = true;
            start_a = false;
            start.enabled = false;
            completed_a = false;
            completed.enabled = false;
        }
        else if (overdue_a == true)
        {
            overdue_a = false;
            overdue.enabled = false;
        }
    }

    public void setSelectedID(Button btn)
    {
        selectedAssignmentid = int.Parse(btn.transform.parent.gameObject.name);
        Debug.Log(selectedAssignmentid);
    }

    public List<GameObject> getAssignmentObjects()
    {
        return gameObjects;
    }
    //get selectedID

    /*public void setInfopopup()
    {
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;
        foreach (Transform child in infoContent.transform)
        {
            if (child.name == "Cname")
            {
                Text sText = child.GetComponentInChildren<Text>();
                sText.text = RC_info[selectedChallengeid]["sender"];
            }
            else if (child.name == "CTime")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = RC_info[selectedChallengeid]["c_time"];
            }
            else if (child.name == "Cscore")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = RC_info[selectedChallengeid]["c_score"];
            }
            else if (child.name == "ScoreMessage")
            {
                Text cText = child.GetComponentInChildren<Text>();
                if (RC_info[selectedChallengeid]["attempted"] == "0")
                {
                    cText.text = "Can you beat this score?";
                }
                else if (int.Parse(RC_info[selectedChallengeid]["c_score"]) <= int.Parse(RC_info[selectedChallengeid]["your_score"]))
                {
                    cText.text = "Good Job!";
                }
                else
                {
                    cText.text = "Better luck next time";
                }
            }
            else if (child.name == "Topic List")
            {
                Text cText = child.GetComponentInChildren<Text>();
                string topics = "";
                foreach (var item in topic_info[selectedChallengeid])
                {
                    topics += item + "\n";
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
            else if (child.name == selectedChallengeid)
            {
                Destroy(child.gameObject);
                start_shift = true;
            }
            else if (start_shift)
            {
                child.localPosition += new Vector3(0, 65, 0);
            }
        }
    }

    public void OnAcceptYes()
    {
        //add code to load new game scene and send challenge id to db
    }*/
}

