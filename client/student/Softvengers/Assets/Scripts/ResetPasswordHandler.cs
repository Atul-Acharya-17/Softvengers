using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordHandler : MonoBehaviour
{
    public InputField currentPasswordField;
    public InputField newPasswordField;
    public InputField confirmationPasswordField;
    

    public void ChangePassword()
    {
        string oldPassword = currentPasswordField.text;
        string password = newPasswordField.text;
        string confirmationPassword = confirmationPasswordField.text;
    }

    public void UpdatePassword(string oldPassword, string newPassword, string confirmationPassword)
    {
        bool check = CheckConfirmationPassword(newPassword, confirmationPassword);

        if (!check)
            return;
        UpdateDatabase(oldPassword, newPassword);
    }

    public void UpdateDatabase(string oldPassword, string newPassword)
    {
        PasswordDetails passwordDetails = new PasswordDetails();
        passwordDetails.oldPassword = oldPassword;
        passwordDetails.newPassword = newPassword;
        passwordDetails.emailID = SecurityToken.Email;

        StartCoroutine(ServerController.Put("http://localhost:5000/student/details/changePassword", passwordDetails.stringify(),
            result =>
            {
                Debug.Log("Done");
            }
            ));

    }
    public bool CheckConfirmationPassword(string password, string confirmationPassword) {
        if (password != confirmationPassword)
        {
            Debug.Log("Password should be same as confirmation password");
            return false;
        }
        return true;
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
}

