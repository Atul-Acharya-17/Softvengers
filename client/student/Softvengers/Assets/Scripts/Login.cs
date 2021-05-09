using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
//using Newtonsoft.Json;   

//[Serializable]
public class Login
{
    public string emailID;
    public string password;
    
    public Login(string name, string pass){
        this.emailID = name;
        this.password = pass;
    }

    // string ---------> JSON

    public string stringify(){

        //return JsonUtility.ToJson(this);
        //var dic = "{'username': this.username, 'password': this.password}";
        return JsonUtility.ToJson(this);
    }


    // JSON ------------> string
    public static Login Parse(string json){
        return JsonUtility.FromJson<Login>(json);
    }


}
