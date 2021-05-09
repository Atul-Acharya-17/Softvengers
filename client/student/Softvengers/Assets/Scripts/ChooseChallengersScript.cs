using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//senderScore
//senderTime
//challenegID
//[] receivers

[System.Serializable]
public class sendChallengers
{
    public string challengeID;
    public float senderTime;
    public double senderScore;
    public string[] receivers;
}
[System.Serializable]
public class SingleStudent
{
    public string emailID;
    public string name;
}

[System.Serializable]
public class StudentList
{
    public SingleStudent[] studentsTut;
}


public class ChooseChallengersScript : MonoBehaviour
{
    public GameObject infoContent;
    public List<SingleStudent> existStud = new List<SingleStudent>();
    public GameObject toggleTemplate;
    public GameObject scroll;
    public GameObject socialMedia;
    
    //public StudentList studs = new StudentList();

    /*public Canvas info;
    public bool info_a = false;
    public GameObject scroll;
    public GameObject toggleTemplate;
    public GameObject infoContent;
    public List<string> selectedStud;
    public List<string> existStud = new List<string> { "A", "B", "C", "D" };
    public List<GameObject> stud_list = new List<GameObject>();
    public void student_list_pop()
    {
        if (info_a == false)
        {
            info_a = true;
            info.enabled = true;
        }
        else if (info == true)
        {
            info_a = false;
            info.enabled = false;
            foreach (var item in stud_list)
            {
                Destroy(item);
            }
            foreach (Transform child in infoContent.transform)
            {
                selectedStud = new List<string>();
                if (child.name=="ChallengerSelectionText")
                {
                    continue;
                }
                else
                {
                    
                }
            }

        }
    }


    public void setInfopopup()
    {
        //302,-79,0
        /*foreach (Transform child in infoContent.transform)
        {
            print(child.name+child.localPosition);
        }
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;

        Vector3 curPos = new Vector3(200, -79, 0);
       
        foreach (var item in existStud)
        {

            var newPC = Instantiate(toggleTemplate, infoContent.transform, false);
            stud_list.Add(newPC);
            newPC.name = item;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                
                if(child.name=="Label")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item; 

                }
            
                

            }
            
            curPos -= new Vector3(0, 40, 0);

            

        }
    }*/

    
    void Awake()
    {
        Debug.Log("Choose challenge");
        Debug.Log(PlayerType.Challengee);
        Debug.Log(Challenge.playerType);

        if (Challenge.playerType == PlayerType.Challenger)
        {
           StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/details/getAllStudents/{0}", SecurityToken.TutGrp),
           result =>
           {
               if (result != null)
               {
                   Debug.Log(result);

                   StudentList slist = JsonUtility.FromJson<StudentList>("{ \"studentsTut\": " + result + "}");

                   existStud = (slist.studentsTut).ToList<SingleStudent>();
                   existStud.Remove(existStud.Single(student => student.emailID == SecurityToken.Email));

                   //print(existStudent.Count);

               }
               else
               {
                   Debug.Log("No sent challenges");

               }
               Setup();
           }
           ));

        }

        else
        {
            Setup();
        }


    }
    void Setup()
    {
        Debug.Log("ChooseL" + Challenge.playerType);
        GameObject sm;
        if (Challenge.playerType == PlayerType.Challengee)
        {
            sm = Instantiate(socialMedia, new Vector3(0, -130, 0), Quaternion.identity);
            scroll.SetActive(false);
        }
        else
        {
            sm = Instantiate(socialMedia, new Vector3(0, -380, 0), Quaternion.identity);
        }
        //302,-79,0

        sm.transform.SetParent(GameObject.FindGameObjectWithTag("ResultPage").transform, false);
        foreach (Transform child in infoContent.transform)
        {
            print(child.name + child.localPosition);
        }
        //Scrollbar s = scroll.GetComponent<Scrollbar>();
        //s.value = 1;

        Vector3 curPos = new Vector3(100, -50, 0);

        //print(existStudent.Count);
        foreach (var item in existStud)
        {
            // print(existStudent.Count);

            var newPC = Instantiate(toggleTemplate, infoContent.transform, false);

            newPC.name = item.emailID;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {

                if (child.name == "Label")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                   
                    cText.text = item.name;
                    print(cText.text);
                    //print(item.emailID);

                }

            }
            curPos -= new Vector3(0, 40, 0);
        }


    }  

    public void onSend()
    {
        List<string> selectedStud = new List<string>();
        foreach (Transform child in infoContent.transform)
        {
            
            if (child.name == "ChoosePlayers")
            {
                continue;
            }
            else
            {
                Toggle tog = child.GetComponent<Toggle>();
                if (tog.isOn== true)
                selectedStud.Add(child.name);
            }
        }

        foreach(string item in selectedStud)
        {
            print(item);
           // selectedStud.Add(item);
        }
        sendChallengers sendObj = new sendChallengers();
        sendObj.receivers = selectedStud.ToArray();
        sendObj.senderScore = ChallengeResultManager.getScore();
        sendObj.senderTime = ChallengeGameController.endTime - ChallengeGameController.startTime;
        //sendObj.senderTime = 10.1f;
        sendObj.challengeID = Challenge.challengeID;
        string json = JsonUtility.ToJson(sendObj);
        print(json);
        StartCoroutine(ServerController.Put("http://localhost:5000/student/challenge/sendChallenge", json,
             result =>
             {
                 if (result != null)
                 {
                     //need to convert into complete object to store challenge id
                     Debug.Log(result);
                     //CreateChallengeResult createchallengeResult = JsonUtility.FromJson<CreateChallengeResult>(result);
                     //challengeID = createchallengeResult.challengeID;
                     //print(challengeID);
                     SceneManager.LoadScene("ChallengeReceived");
                 }
                 else
                 {
                     Debug.Log("Error in sending!");

                 }
             }
        ));
       

    }




}
