using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UniverseScript : MonoBehaviour
{
    public GameObject progressBar;
    public int screenWidth = 640;
    public int headingWidth = 30;

    // Start is called before the first frame update
    void Start()
    {

        Dictionary<int, Dictionary<string, string>> Universe_prog_info = ProgressController.Universe_prog_info[ProgressController.selectedUniverse];
        
        int numBar = Universe_prog_info.Count;
        int decrementFactor = ((screenWidth - 5 * headingWidth) / numBar);
        int startPos = screenWidth / 2 - 5 * headingWidth - 10;
        Vector3 curpos = new Vector3(0, startPos, 0);

        Text heading = GameObject.Find("ProgressHeadingText").GetComponent<Text>();
        heading.text =  Multiverse.getUniverses()[ProgressController.selectedUniverse];
        // ProgressController.Multiverse_prog_info[ProgressController.selectedUniverse]["name"]

        List<string> SSNames = Multiverse.getSolarSystems(ProgressController.selectedUniverse);
        for (int x = 0; x < numBar; x++)
        {
            var newBar = Instantiate(progressBar, curpos, Quaternion.identity);
            newBar.transform.SetParent(gameObject.transform, false);
            newBar.name = x.ToString();
            Button btn = newBar.GetComponent<Button>();
            btn.onClick.AddListener(UniverseBarClick);


            Text text = newBar.GetComponentInChildren<Text>();
            if (float.Parse(Universe_prog_info[x]["value"])>=0.99){
                Universe_prog_info[x]["value"] = "1";
            }
            text.text = SSNames[x]+ ":    "+((float.Parse(Universe_prog_info[x]["value"]))*100).ToString()+" %";

            Slider slider = btn.GetComponentInChildren<Slider>();
            slider.value = float.Parse(Universe_prog_info[x]["value"]);

            if (slider.value < 1)
            {

                if (slider.value > 0.4)
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Universe_prog_info[x]["value"]));
                }

                else
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Universe_prog_info[x]["value"]));
                }
            }


            curpos -= new Vector3(0, decrementFactor, 0);

        }

       
        Debug.Log("Created");
    }

    void UniverseBarClick()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        ProgressController.selectedSs = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene("SolarSystemProgress");
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

}
