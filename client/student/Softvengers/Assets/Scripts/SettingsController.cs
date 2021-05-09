using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Player playerData;
    public Slider slider;
    private float soundLevel;

    public void ChangeAvatar(int avatarID)
    {
        playerData.colorChoice = avatarID;
        PlayerChoice playerChoice = new PlayerChoice();
        playerChoice.emailID = SecurityToken.Email;
        playerChoice.avatar = avatarID;

        //Need to decide how to store volume
        playerChoice.volume = 5;

        StartCoroutine(ServerController.Put("http://localhost:5000/student/details/updateStudent", playerChoice.stringify(),
            result =>
            {
                if (result != null)
                {
                    Debug.Log(result);
                }
            }
            ));
    }

    public void ChangeSound()
    {
        this.soundLevel = slider.GetComponent<Slider>().value;
        Debug.Log(this.soundLevel);
    }
}

public struct PlayerChoice
{
    public string emailID;
    public int avatar;
    public int volume;

    public string stringify()
    {

        //return JsonUtility.ToJson(this);
        //var dic = "{'username': this.username, 'password': this.password}";
        return JsonUtility.ToJson(this);
    }
}
