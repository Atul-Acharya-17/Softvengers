using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordChanger : MonoBehaviour
{
    /*
    public InputField currentPasswordField;
    public InputField newPasswordField;
    public InputField confirmationPasswordField;


    public void ChangePassword()
    {
        string oldPassword = currentPasswordField.text;
        string password = newPasswordField.text;
        string confirmationPassword = confirmationPasswordField.text;

        if (password != confirmationPassword)
        {
            Debug.Log("Password should be same as confirmation password");
            return;
        }

        else
        {
            PasswordDetails passwordDetails = new PasswordDetails();
            passwordDetails.oldPassword = oldPassword;
            passwordDetails.newPassword = password;
            passwordDetails.emailID = SecurityToken.Email;

            StartCoroutine(ServerController.Post("http://localhost:5000/student/login", passwordDetails.stringify(),
                result =>
                {
                    Debug.Log("Done");
                }
                ));
            
        }

    }

}

public struct PasswordDetails
{
    public string emailID;
    public string newPassword;
    public string oldPassword;


    public string stringify()
    {

        //return JsonUtility.ToJson(this);
        return JsonUtility.ToJson(this);
    }


    // JSON ------------> string
    public static Login Parse(string json)
    {
        return JsonUtility.FromJson<Login>(json);
    }
    */
}



