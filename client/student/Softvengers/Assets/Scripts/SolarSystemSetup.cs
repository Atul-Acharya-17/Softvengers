using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, string> SS_prog_info = ProgressController.Universe_prog_info[ProgressController.selectedUniverse][ProgressController.selectedSs];
        string[] levels = new string[3] { "basic", "intermediate", "advanced" };

        Text heading = GameObject.Find("ProgressSubHeading").GetComponent<Text>();
        heading.text = Multiverse.getSolarSystems(ProgressController.selectedUniverse)[ProgressController.selectedSs];
        foreach (string level in levels)
        {
            if (float.Parse(SS_prog_info[level])!= 0)
            {
                chooseBadgeImage(float.Parse(SS_prog_info[level]), level);
            }
        }

    }

    void chooseBadgeImage(float value, string planet)
    {
        Sprite sprite;
        Image image;
        GameObject badge=new GameObject("init");

        switch(planet)
        {
            case "basic":
                     badge = GameObject.Find("BasicBadge");
                     break;
            case "intermediate":
                     badge = GameObject.Find("IntermediateBadge");
                     break;
            case "advanced":
                     badge = GameObject.Find("AdvancedBadge");
                     break;
        }

        Debug.Log(badge.name);

        switch (value)
        {
            case 0.11f:
                  sprite = Resources.Load<Sprite>("Images/bronze_shield");
                  image = badge.GetComponent<Image>();
                  image.sprite = sprite;
                  break;
            case 0.22f:
                sprite = Resources.Load<Sprite>("Images/silver_shield");
                image = badge.GetComponent<Image>();
                image.sprite = sprite;
                break;
            case 0.33f:
                sprite = Resources.Load<Sprite>("Images/gold_shield");
                image = badge.GetComponent<Image>();
                image.sprite = sprite;
                break;

        }
    }
}
