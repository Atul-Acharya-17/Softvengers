using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image avatarImage;
    public Player playerData;

    public List<Sprite> characters;
    void Start()
    {
        avatarImage.GetComponent<Image>().sprite = characters[playerData.colorChoice];
    }

    
}
