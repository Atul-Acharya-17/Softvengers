using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Player playerData;
    public GameObject errorMsg;
    public AudioSource audioSource;

    public void CheckValidLogin()
    {
        string emailID = usernameField.text;
        string password = passwordField.text;

        emailID = emailID.Trim();

        string[] splitString = emailID.Split('@');

        if (splitString.Length > 1)
        {
            emailID = splitString[0].ToUpper() + "@" + splitString[1].ToLower();
        }

        if (audioSource != null)
            audioSource.Play();

        AuthenticateDetails(emailID, password);
    }

    public void Start()
    {
        if (errorMsg != null)
            errorMsg.SetActive(false);
    }

    public void AuthenticateDetails(string emailID, string password)
    {

        Login player = new Login(emailID, password);

        StartCoroutine(ServerController.Post("http://localhost:5000/student/login", player.stringify(),
            result =>
            {
                if (result != null)
                {   
                    LoginResult loginResult = JsonUtility.FromJson<LoginResult>(result);

                    if (loginResult.message)
                    {
                        Debug.Log("Login success");
                        SecurityToken.Token = loginResult.token;
                        SecurityToken.Email = player.emailID;

                        StartCoroutine(ServerController.Get(string.Format("http://localhost:5000/student/details/getStudent?emailID={0}", player.emailID),
                           
                            playerDataResult =>
                            {
                                if (playerDataResult != null && playerData != null)
                                {
                                    Progress progress = JsonUtility.FromJson<Progress>(playerDataResult);
                                    playerData.universePogress = progress.conqueredUniverse;
                                    playerData.solarSystemProgress = progress.conqueredSolarSystem;
                                    playerData.planetProgress = progress.conqueredPlanet;
                                    playerData.colorChoice = progress.avatar;
                                    playerData.totalScore = progress.totalScore;
                                    Debug.Log(playerData.universePogress);
                                    Debug.Log(playerData.solarSystemProgress);
                                    Debug.Log(playerData.planetProgress);
                                    SecurityToken.TutGrp = progress.tutGrp;
                                    Debug.Log(SecurityToken.TutGrp);
                                    SecurityToken.MatricNo = progress.matricNo;
                                    SceneManager.LoadScene("HomePageScene");
                                }

                                else
                                {
                                    Debug.Log("Could not retrieve player data");
                                    
                                }
                                
                            }                          
                            ));
                    }

                    else
                    {
                        SecurityToken.Email = "";
                        if (errorMsg != null)
                            errorMsg.SetActive(true);
                        Debug.Log("Invalid Credentials");
                    }
                }
            }
            ));
    }

    public void onClickDismiss()
    {
        errorMsg.SetActive(false);
    }

}



public struct LoginResult
{
    public bool message;
    public string token;
}

public struct Progress
{
    public int conqueredUniverse;
    public int conqueredSolarSystem;
    public int conqueredPlanet;
    public int avatar;
    public double totalScore;
    public string tutGrp;
    public string matricNo;
}
