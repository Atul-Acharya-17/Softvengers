using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Player playerData;
    public List<Material> colors;
    void Start()
    {
        int colorChoice = playerData.colorChoice;
        Material color = colors[colorChoice];
        //Debug.Log(color);

        Transform spaceShip = this.transform.Find("jet");
        Renderer renderer = spaceShip.GetComponent<Renderer>();
        renderer.material = color;
        
    }

}
