using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetNavigation : ExplorationController
{
    // Start is called before the first frame update
    public List<GameObject> spaceObjects;
    void Start()
    {
        this.names = new List<string>() {
            {"Easy"},
            {"Medium"},
            {"Hard"}
        };

        this.RenderObjects();
    }

    public override void RenderObjects()
    {
        float x = -4.0f;
        float z = 7.0f;
        float y = -0.3f;

        for (int i = 0; i < this.names.Count; ++i)
        {
            GameObject planet = Instantiate(spaceObjects[i], new Vector3(x, y, z), Quaternion.identity);
            x += 4;
            Transform canvas = planet.transform.Find("Canvas");

            Transform button = canvas.transform.Find("Button");
            int planetID = i;
            button.GetComponent<Button>().onClick.AddListener(() => { this.ExploreWorld(planetID); });

            ColorBlock colorVar = button.GetComponent<Button>().colors;
            if (this.IsUnlocked(planetID))
            {
               
                colorVar.highlightedColor = new Color(0f, 255f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(0f, 255f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(0f, 255f / 255f, 10f / 255f, 150f / 255f);

            }
            else
            {
                
                colorVar.highlightedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 150f / 255f);
            }
            button.GetComponent<Button>().colors = colorVar;
            Debug.Log(button.GetComponent<Button>().colors.highlightedColor);
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = this.names[planetID];
        }

    }

    public override bool IsUnlocked(int spaceObjectID)
    {
        if (this.navigation.universeSelected < this.playerData.universePogress)
            return true;
        if (this.navigation.universeSelected == this.playerData.universePogress && this.navigation.solarSystemSelected < this.playerData.solarSystemProgress)
            return true;

        if (this.navigation.universeSelected == this.playerData.universePogress && this.navigation.solarSystemSelected == this.playerData.solarSystemProgress && spaceObjectID <= this.playerData.planetProgress)
            return true;
        return false;
    }

    public override void ExploreWorld(int spaceObjectID)
    {
        if (this.IsUnlocked(spaceObjectID))
        {
            this.navigation.planetSelected = spaceObjectID;
            this.ChangeScene("SoloGamePlayScene");
        }
        else
            Debug.Log("Locked");
    }

}
