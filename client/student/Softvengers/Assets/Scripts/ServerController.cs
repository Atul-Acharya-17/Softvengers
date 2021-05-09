using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerController : MonoBehaviour
{
    public static IEnumerator Post(string url, string jsonData, System.Action<string> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + SecurityToken.Token);
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
                    
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    // Do something with the result
                    if (callback != null) { callback.Invoke(result); }

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                    if (callback != null) { callback.Invoke(null); }
                }
            }
        }
    }

    public static IEnumerator Get(string url, System.Action<string> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + SecurityToken.Token);
            //www.uploadHandler.contentType = "application/json";
            //www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                yield return null;
            }

            else
            {
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.data);
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    if (callback != null) { callback.Invoke(result); }

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                    if (callback != null) { callback.Invoke(null); }
                }
            }
        }
    }


    public static IEnumerator Put(string url, string jsonData, System.Action<string> callback = null)
    {
        Debug.Log(jsonData);
        using (UnityWebRequest www = UnityWebRequest.Put(url, jsonData))
        {
            www.method = "PATCH";
            www.SetRequestHeader("content-type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + SecurityToken.Token);
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                yield return null;
            }

            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    if (callback != null)
                    {
                        callback.Invoke(result);

                    }
                    else
                    {
                        //handle the problem
                        Debug.Log("Error! data couldn't get.");
                        if (callback != null)
                        {
                            callback.Invoke(null);
                        }
                    }
                }
            }
        }
    }
    

    public static IEnumerator Delete(string url, System.Action<string> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete(url))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + SecurityToken.Token);
            //www.uploadHandler.contentType = "application/json";
            //www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                yield return null;
            }

            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    yield return result;
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                    yield return null;
                }
            }
        }
    }
}
