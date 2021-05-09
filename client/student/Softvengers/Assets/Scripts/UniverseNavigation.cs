using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniverseNavigation : ExplorationController
{
    // Start is called before the first frame update
    protected float radius;
    public GameObject spaceObject;
    private List<GameObject> universes;
    void Start()
    {
        this.radius = 65.0f;
        names = Multiverse.getUniverses();

        this.RenderObjects();
    }

    public override void RenderObjects()
    {
        universes = new List<GameObject>();
        int numObjects = this.names.Count;

        float increment = 360.0f / numObjects;
        float angle = 0.0f;

        for (int objectID=0; objectID<numObjects; ++objectID)
        {
            float x = radius * Mathf.Sin(0.01745f * angle);
            float z = radius * Mathf.Cos(0.01745f * angle);

            if (spaceObject == null)
            {
                universes.Add(new GameObject());
                continue;
            }
            GameObject galaxy = Instantiate(spaceObject, new Vector3(x, -5f, z), Quaternion.identity);
            universes.Add(galaxy);
            Transform canvas = galaxy.transform.Find("Canvas");
            Transform button = canvas.transform.Find("Button");
            int currentUniverse = objectID;
            button.GetComponent<Button>().onClick.AddListener(() => { this.ExploreWorld(currentUniverse); });
            ColorBlock colorVar = button.GetComponent<Button>().colors;

            if (this.IsUnlocked(currentUniverse))
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
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = names[objectID];

            angle += increment;

        }

    }

    public override bool IsUnlocked(int spaceObjectID)
    {
        if (spaceObjectID <= this.playerData.universePogress)
            return true;
        return false;
    }

    public override void ExploreWorld(int spaceObjectID)
    {
        if (this.IsUnlocked(spaceObjectID))
        {
            this.navigation.universeSelected = spaceObjectID;
            Debug.Log("Unlocked");
            this.ChangeScene("SolarSystemScene");
        }
        else
            Debug.Log("Locked");
    }

    public List<GameObject> GetGameObjects()
    {
        return this.universes;
    }
}
