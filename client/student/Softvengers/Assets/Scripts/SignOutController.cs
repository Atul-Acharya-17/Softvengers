using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignOutController : MonoBehaviour
{

    public AudioSource audioSource;
    public void SignOut()
    {
        SecurityToken.Email = "";
        SecurityToken.Token = "";

        if (audioSource != null)
        {
            audioSource.Play();
        }

        SceneManager.LoadScene("LoginUI");
    }
}
