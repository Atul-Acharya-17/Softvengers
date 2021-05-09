using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MultiverseProgSetup : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject Badge;
    public int numBar = ProgressController.numUniverse;
    public int screenWidth = 640;
    public int headingWidth = 30;

    // Start is called before the first frame update
    public void Setup()
    {
       
        int decrementFactor = ((screenWidth - 4* headingWidth) / numBar);
        int startPos = screenWidth / 2 - 4* headingWidth;
        Vector3 curpos = new Vector3(0,startPos,0);
        Dictionary<int, string> badge_img = new Dictionary<int, string>()
        {
            {0,"mind_stone" },
            {1,"power_stone"},
            {2,"reality_stone"},
            {3,"soul_stone"},
            {4,"space_stone"},
            {5,"time_stone"}
        };

       
        Dictionary<int, Dictionary<string, string>> Multiverse_prog_info = ProgressController.Multiverse_prog_info;
        List<string> uniNames = Multiverse.getUniverses();
        for (int x = 0; x < numBar; x++)
        {
            var newBar = Instantiate(progressBar, curpos, Quaternion.identity);
            newBar.transform.SetParent(gameObject.transform,false);
            newBar.name = x.ToString();
            Button btn = newBar.GetComponent<Button>();
            btn.onClick.AddListener(MultiverseBarClick);

            Text text = newBar.GetComponentInChildren<Text>();
            if (float.Parse(Multiverse_prog_info[x]["value"])>=0.99){
                Multiverse_prog_info[x]["value"] = "1";
            }
            text.text = uniNames[x]+":    " + ((float.Parse(Multiverse_prog_info[x]["value"])) * 100).ToString() + " %"; ;

            Slider slider = btn.GetComponentInChildren<Slider>();
            slider.value = float.Parse(Multiverse_prog_info[x]["value"]);

            if (slider.value >=0.99)
            {
                var newBadge = Instantiate(Badge, curpos + new Vector3(550, 0, 0), new Quaternion(0, 0, 90, 1));

                var sprite = Resources.Load<Sprite>("Images/" + badge_img[x]);
                Image image = newBadge.GetComponent<Image>();
                image.sprite = sprite;


                newBadge.transform.SetParent(gameObject.transform, false);
                var rot = newBadge.GetComponent<RectTransform>();
                rot.localRotation = new Quaternion(0, 0, Mathf.Sin(0.01745f * 90), 1);

            }


            if (slider.value < 1)
            {

                if (slider.value > 0.4)
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Multiverse_prog_info[x]["value"]));
                }

                else
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Multiverse_prog_info[x]["value"]));
                }
            }

            curpos -= new Vector3(0, decrementFactor, 0);

        }

         
        Debug.Log("Created");
    }

    void MultiverseBarClick()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        ProgressController.selectedUniverse = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene("UniverseProgress");
    }


    Gradient createGrad()
    {
        Gradient gradient;
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color(0.61f, 0.92f, 0.72f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(0.18f, 0.89f, 0.10f);
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 0.5f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        return gradient;

    }

    Color calcColor(float val)
    {
        Gradient gradient = createGrad();

        // What's the color at the relative time 0.25 (25 %) ?
        return gradient.Evaluate(val);


    }

    [System.Serializable]
    public class PlanetProgress
    {
        public string identifier;
        public int maxCorrect;

    }

    [System.Serializable]
    public class Progress
    {
        public PlanetProgress[] progress;

    }


    public void Awake()
    {
        Dictionary<int, Dictionary<int, Dictionary<string, string>>> Universe_prog_info = new Dictionary<int, Dictionary<int, Dictionary<string, string>>>();
        Dictionary<int, Dictionary<string, string>> uni_stuff = new Dictionary<int, Dictionary<string, string>>();
        Dictionary <int,float> solar_stuff = new Dictionary<int,float>();
        // "emailID=SRISH@e.ntu.edu.sg" use this for immediate testing
        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/details/getProgress?emailID={0}", SecurityToken.Email),
        result =>
        {
        if (result != null)
        {
            Progress FullProgress = JsonUtility.FromJson<Progress>(result);
            print(FullProgress.progress[0].identifier);
            print(FullProgress.progress.Length);
            
            foreach (var obj in FullProgress.progress)
            {
                string id = obj.identifier.Substring(1, obj.identifier.Length - 2);
                string[] ids = id.Split(',');
                float badgeVal = 0.0f;
                if (obj.maxCorrect == 10)
                {
                    badgeVal = 0.33f;
                }
                else if(obj.maxCorrect >=7 )
                {
                        badgeVal = 0.22f;
                }
                    else if(obj.maxCorrect>=5)
                    {
                        badgeVal = 0.11f;
                    }
                    string badge = badgeVal.ToString();
                
                    if (Universe_prog_info.ContainsKey(int.Parse(ids[0])) == false)
                {
                        
                        Dictionary<int, Dictionary<string, string>> Solar_prog_info = new Dictionary<int, Dictionary<string, string>>();
                        Solar_prog_info.Add(int.Parse(ids[1]),new Dictionary<string, string>() );
                        int ssno = int.Parse(ids[1]) + 1;
                        Solar_prog_info[int.Parse(ids[1])].Add("name","Solar System"+ssno.ToString());
                        
                        
                        Solar_prog_info[int.Parse(ids[1])].Add("value", badge);
                        if (int.Parse(ids[2]) == 0)
                        {
                            Solar_prog_info[int.Parse(ids[1])].Add("basic", badge);
                        }
                        else if(int.Parse(ids[2]) == 1)
                        {
                            Solar_prog_info[int.Parse(ids[1])].Add("intermediate", badge);
                        }
                        else 
                        {
                            Solar_prog_info[int.Parse(ids[1])].Add("advanced", badge);
                        }

                        Universe_prog_info.Add(int.Parse(ids[0]),Solar_prog_info);
                        
                    
                }
                else
                 {
                    if(Universe_prog_info[int.Parse(ids[0])].ContainsKey(int.Parse(ids[1])) == true)
                        {
                            if(int.Parse(ids[2])==0)
                            {
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])].Add("basic", badge);
                                float val = float.Parse(Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"]);
                                val = val + badgeVal;
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"] = val.ToString();
                            }
                            else if(int.Parse(ids[2]) == 1)
                            {
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])].Add("intermediate", badge);
                               
                                float val = float.Parse(Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"]);
                                val = val + badgeVal;
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"] = val.ToString();
                            }
                            else
                            {
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])].Add("advanced", badge);
                                
                                float val = float.Parse(Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"]);
                                val = val + badgeVal;
                                Universe_prog_info[int.Parse(ids[0])][int.Parse(ids[1])]["value"] = val.ToString();
                            }
                        }
                        else
                        {
                            Dictionary<int, Dictionary<string, string>> Solar_prog_info = new Dictionary<int, Dictionary<string, string>>();
                            Solar_prog_info.Add(int.Parse(ids[1]), new Dictionary<string, string>());
                            int ssno = int.Parse(ids[1]) + 1;
                            Solar_prog_info[int.Parse(ids[1])].Add("name", "Solar System" + ssno.ToString());
                            Solar_prog_info[int.Parse(ids[1])].Add("value", badge);
                            if (int.Parse(ids[2]) == 0)
                            {
                                Solar_prog_info[int.Parse(ids[1])].Add("basic",badge);
                            }
                            else if (int.Parse(ids[2]) == 1)
                            {
                                Solar_prog_info[int.Parse(ids[1])].Add("intermediate",badge);
                            }
                            else
                            {
                                Solar_prog_info[int.Parse(ids[1])].Add("advanced",badge);
                            }
                            Universe_prog_info[int.Parse(ids[0])].Add(int.Parse(ids[1]), Solar_prog_info[int.Parse(ids[1])]);

                        }
                 }
             }
           
                
            foreach (var j in Universe_prog_info)
                {
                    int counter = 0;
                    float sum = 0.0f;
                    foreach(var i in j.Value)
                    {
                        sum = float.Parse((i.Value)["value"]) + sum;
                        counter++;
                    }
                    Dictionary<string, string> uni_val = new Dictionary<string, string>();
                    uni_val.Add("name", "Universe" + (j.Key + 1).ToString());
                    float valAdd = sum / counter;
                    uni_val.Add("value", valAdd.ToString());
                    uni_stuff.Add(j.Key, uni_val);
                }

        foreach(var item in uni_stuff)
                {
                    print(item.Key);
                    foreach(var val in item.Value)
                    {
                        print(val.Key);
                        print(val.Value);
                    }
                }

         }
          else
            {
                Debug.Log("No questions!");

            }

            /*foreach(var item in Universe_prog_info)
            {
                print(item.Key);
                foreach(var val in item.Value)
                {
                    print(val.Key);
                    foreach(var v in val.Value)
                    {
                        print(v.Key);
                        print(v.Value);                    }
                }
            }*/

            ProgressController.Universe_prog_info = Universe_prog_info;
            ProgressController.Multiverse_prog_info = uni_stuff;
            Setup();
        }
        ));
    }

}
