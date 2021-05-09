using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ExplorationController : MonoBehaviour
{
    // Start is called before the first frame update
    protected List<string> names;
    public AudioSource audioSource;
    public Player playerData;
    public Navigation navigation;
    

    public void ChangeScene(string sceneName)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        SceneManager.LoadScene(sceneName);
    }
    public abstract bool IsUnlocked(int spaceObjectID);
    public abstract void RenderObjects();
    public abstract void ExploreWorld(int spaceObjectID);

}
