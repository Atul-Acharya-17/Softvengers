using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class TopicInfo
{
    
    public int universe;
    public int solarSystem;
    public int planet;
    public int noOfQuestions;
}

[System.Serializable]
public class ChallengeInfo
{
    public string senderEmailID;
    public string challengeName;
    public List<TopicInfo> topics;
}

[System.Serializable]
public class CreateChallengeResult
{
    public string challengeID;
    public qsts[] questions;


}

public class CreateChallengeScreen : MonoBehaviour
{
    public int nextOptionsid;
    public GameObject OptionsCard;
    //public static string challengeID;
    // Start is called before the first frame update

    public Dictionary<int, List<string>> Universe_SS_mapping = new Dictionary<int, List<string>>()
    {
        { 1, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 0, new List<string>{"SS1","SS2","SS3","SS4"} },
        { 2, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 3, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 4, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 5, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} }
    };

    void Start()
    {
        nextOptionsid = 1;
        //print(Universe_SS_mapping[0][0]);
        GameObject SSDD = GameObject.Find("Solar System Dropdown").gameObject;
        Dropdown SSDropdown = SSDD.GetComponent<Dropdown>();
        changeSSOptions(SSDropdown);

        

    }

    public void OnAddClick()
    {
        GameObject AddButton = EventSystem.current.currentSelectedGameObject;
        AddButton.transform.localPosition -= new Vector3(0,195f,0);
        GameObject firstCard = GameObject.Find("0");
        if(nextOptionsid%4==0)
        {
            RectTransform scroll=gameObject.GetComponent<RectTransform>();
            scroll.sizeDelta += new Vector2(0, 1000);
            foreach(Transform child in gameObject.transform)
            {
                Debug.Log(child.name);
                child.localPosition+= new Vector3(0,500,0);
            }
        }
        var newOptionsCard = Instantiate(OptionsCard,gameObject.transform,false);
        newOptionsCard.transform.localPosition = new Vector3(firstCard.transform.localPosition.x,AddButton.transform.localPosition.y+20f, 0);
        newOptionsCard.name = nextOptionsid.ToString();
        Button btn = newOptionsCard.GetComponentInChildren<Button>();
        btn.onClick.AddListener(OnRemoveClick);
        Dropdown UniDD = newOptionsCard.transform.Find("Universe Dropdown").gameObject.GetComponent<Dropdown>();
        changeSSOptions(UniDD);
        UniDD.onValueChanged.AddListener(delegate {
            changeSSOptions(UniDD);
        });
        nextOptionsid += 1;

    }

    public void OnRemoveClick()
    {
        GameObject RemoveButton = EventSystem.current.currentSelectedGameObject;
        GameObject toRemove = RemoveButton.transform.parent.gameObject;
        int ind= int.Parse(toRemove.transform.name);
        Destroy(toRemove);
        foreach (Transform child in gameObject.transform)
        {
            if (child.name=="AddButton")
            {
                child.localPosition += new Vector3(0,195,0);
            }
            else if (child.name == "ChallengeNameHeading" || child.name== "ChallengeNameIF")
            {
                continue;
            }
            else if (int.Parse(child.name)>ind)
            {
                child.localPosition += new Vector3(0, 175, 0);
            }
        }
    }

    public void changeSSOptions(Dropdown change)
    {
        GameObject currentDropdown = change.gameObject;
        GameObject currentCard = currentDropdown.transform.parent.gameObject;
        GameObject SSObject = currentCard.transform.Find("Solar System Dropdown").gameObject;
        Dropdown SSDropdown = SSObject.GetComponent<Dropdown>();
        SSDropdown.ClearOptions();
        
        foreach (string SS in Multiverse.getSolarSystems(change.value))
        {
            Dropdown.OptionData NewData = new Dropdown.OptionData();
            NewData.text = SS;
            SSDropdown.options.Add(NewData);
        }

        SSDropdown.value = 0;
        SSDropdown.captionText.text = Multiverse.getSolarSystems(change.value)[0];




    }

    public void OnSubmitClick()
    {
        List <List<string>> submitInfo= new List<List<string>>();
        string[] attri = new string[4] { "Universe Dropdown", "Solar System Dropdown", "Qnum", "Dl" };
        //TopicInfo topicInfo = new TopicInfo();
        ChallengeInfo challengeInfo = new ChallengeInfo();
        challengeInfo.topics = new List<TopicInfo>();


        foreach (Transform child in transform)
        {
            if (child.name=="AddButton" || child.name== "ChallengeNameHeading")
            {
                continue;
            }
            else if(child.name=="ChallengeNameIF")
            {
                GameObject cname = child.transform.Find("Text").gameObject;
                Text cNameText = cname.GetComponentInChildren<Text>();
                //print(cNameText.text);
                Debug.Log("Searching for error");
                challengeInfo.challengeName = cNameText.text;
                Challenge.challengeName = cNameText.text;
                Debug.Log(Challenge.challengeName);
                if (Challenge.challengeName == "" || Challenge.challengeName == null)
                {
                    Challenge.challengeName = "Challenge by " + SecurityToken.Email.Substring(0, SecurityToken.Email.LastIndexOf('@'));
                }
                continue;
                
            }
            // List<string> tempList = new List<string>();
            TopicInfo topicInfo = new TopicInfo();
            for (int i = 0;i< 4;i++)
            {

                GameObject DropDownObj = child.Find(attri[i]).gameObject;
                Dropdown DropdownComp = DropDownObj.GetComponent<Dropdown>();
                switch(attri[i])
                {
                    case "Universe Dropdown": topicInfo.universe = DropdownComp.value;
                        break;
                    case "Solar System Dropdown":
                        topicInfo.solarSystem = DropdownComp.value;
                        break;
                    case "Qnum":
                        topicInfo.noOfQuestions = int.Parse(DropdownComp.captionText.text);
                        //topicInfo.noOfQuestions = 1;
                        break;
                    case "Dl":
                        topicInfo.planet= DropdownComp.value;
                        break;



                }

                
                //tempList.Add(DropdownComp.captionText.text);
            }
            challengeInfo.topics.Add(topicInfo);
            //submitInfo.Add(tempList);

        }

        //challengeInfo.senderEmailID = SecurityToken.Email;
        challengeInfo.senderEmailID = SecurityToken.Email;
        string json = JsonUtility.ToJson(challengeInfo);
        print(json);
        StartCoroutine(ServerController.Post("http://localhost:5000/student/challenge/createChallenge",json,
             result =>
             {
                 if (result != null)
                 {
                     //need to convert into complete object to store challenge id
                     Debug.Log(result);
                     CreateChallengeResult createchallengeResult = JsonUtility.FromJson<CreateChallengeResult>(result);
                     Challenge.challengeID = createchallengeResult.challengeID;
                     print(Challenge.challengeID);
                     Challenge.playerType = PlayerType.Challenger;


                     SceneManager.LoadScene("ChallengeGamePlayScene");
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
        /*foreach (var item in challengeInfo.topics )
        {
            print(item.universe);
            print(item.solarSystem);
            print(item.planet);
            print(item.noOfQuestions);
        }

        print(challengeInfo.senderEmailID);
        print(challengeInfo.challengeName);*/




    }
}
