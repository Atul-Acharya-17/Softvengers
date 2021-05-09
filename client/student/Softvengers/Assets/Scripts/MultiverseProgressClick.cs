using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MultiverseProgressClick : MonoBehaviour
{

    public AudioSource backButtonAudio;
    public void BackbuttonClick()
    {
        backButtonAudio.Play();
        SceneManager.LoadScene("MultiverseProgress");
    }
}
