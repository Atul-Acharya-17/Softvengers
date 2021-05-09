using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource buttonAudio;
    public void updateScene(string sceneName)
    {
        if (buttonAudio != null)
            buttonAudio.Play();
        SceneManager.LoadScene(sceneName); 
    }

   
}
