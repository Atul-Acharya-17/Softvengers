using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniverseProgressScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource backButtonAudio;
    public void BackbuttonClick()
    {
        backButtonAudio.Play();
        SceneManager.LoadScene("UniverseProgress");
    }
}
