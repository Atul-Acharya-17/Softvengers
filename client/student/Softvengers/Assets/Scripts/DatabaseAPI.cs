using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DatabaseAPI : MonoBehaviour
{

    /*
    public InputField username;
    public InputField password;
    public Player playerData;


    public void checkUser(){

        string Name = username.text;
        string pass = password.text;
        Debug.Log(Name);
        Debug.Log(pass);
        Login player = new Login(Name, pass);
        StartCoroutine(Post("http://localhost:5000/student/login", player));
    }


    public IEnumerator Post(string url, Login player)
    {
        var jsonData = player.stringify();
        Debug.Log(jsonData);


        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }

            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    LoginResult loginResult = JsonUtility.FromJson<LoginResult>(result);
                    Debug.Log(result);
                    if (loginResult.message)
                    {
                        playerData.token = loginResult.token;
                        StartCoroutine(Read("http://localhost:5000/student/details/getStudent", player));
                    }

                    else
                    {
                        Debug.Log("Invalid Credentials");
                    }

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    public IEnumerator Read(string url, Login player)
    {

        Debug.Log("Hello");
        var jsonData = player.stringify();
        Debug.Log("Hello2");
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();


            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }

            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Debug.Log(result);
                    Progress progress = JsonUtility.FromJson<Progress>(result);
                    playerData.universePogress = progress.conqueredUniverse;
                    playerData.solarSystemProgress = progress.conqueredSolarSystem;
                    playerData.planetProgress = progress.conqueredPlanet;
                    playerData.colorChoice = progress.avatar;
                    Debug.Log(playerData.universePogress);
                    Debug.Log(playerData.solarSystemProgress);
                    Debug.Log(playerData.planetProgress);
                    ChangeScene();
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("HomePageScene");
    }
    */
}

/*
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
}
*/
