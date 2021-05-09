using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForumController : MonoBehaviour
{
    public InputField forumPost;
    public GameObject Post;

    private List<GameObject> Posts;

    private float lastY;
    public float startY;
    public float incrementY;

    void Start()
    {
        Posts = new List<GameObject>();
        lastY = startY;
        GetPosts();
    }

    void GetPosts()
    {
        StartCoroutine(ServerController.Get("http://localhost:5000/student/posts/getPosts",
            result =>
            {
                Debug.Log(result);
                if (result != null)
                {
                    PostList posts = JsonUtility.FromJson<PostList>("{ \"posts\": " + result + "}");
                    //List<PostData> posts = JsonUtility.FromJson <List<PostData>> (result);
                    Debug.Log(posts.posts.Count);
                    Debug.Log("Hello");
                    float x = -44;
                    float z = 0;
                    foreach (PostData post in posts.posts)
                    {
                        GameObject postUI = Instantiate(Post, new Vector3(x, lastY, z), Quaternion.identity);
                        postUI.transform.SetParent(GameObject.FindGameObjectWithTag("Posts").transform, false);
                        Posts.Add(postUI);
                        lastY -= incrementY;
                        postUI.transform.Find("Email").GetComponent<Text>().text = post.emailID;
                        postUI.transform.Find("Text").GetComponent<Text>().text = post.text;
                        lastY -= incrementY;
                    }
                }
            }
            ));
    }

    public void PostMessage()
    {

        // Update DataBase
        // If successful then done
        PostData postData = new PostData();
        postData.emailID = SecurityToken.Email;
        postData.text = forumPost.text;

        StartCoroutine(ServerController.Post("http://localhost:5000/student/posts/createPost", postData.stringify(),
            result =>
            {
                Debug.Log(result);
            }));

        float x = -44;
        float z = 0;
        GameObject post = Instantiate(Post, new Vector3(x, lastY, z), Quaternion.identity);
        post.transform.SetParent(GameObject.FindGameObjectWithTag("Posts").transform, false);
        lastY -= incrementY;// Should Decrement Y
        this.Posts.Add(post);

        for (int i = this.Posts.Count - 1; i > 0; --i)
        {
            GameObject curPost = this.Posts[i];
            GameObject prevPost = this.Posts[i - 1];

            curPost.transform.Find("Email").GetComponent<Text>().text = prevPost.transform.Find("Email").GetComponent<Text>().text;
            curPost.transform.Find("Text").GetComponent<Text>().text = prevPost.transform.Find("Text").GetComponent<Text>().text;
        }
        this.Posts[0].transform.Find("Email").GetComponent<Text>().text = SecurityToken.Email;
        this.Posts[0].transform.Find("Text").GetComponent<Text>().text = forumPost.text;
    }
}

[System.Serializable]
class PostData
{
    public string emailID;
    public string text;

    public string stringify()
    {

        //return JsonUtility.ToJson(this);
        //var dic = "{'username': this.username, 'password': this.password}";
        return JsonUtility.ToJson(this);
    }
}

class PostList
{
    public List<PostData> posts;
}
