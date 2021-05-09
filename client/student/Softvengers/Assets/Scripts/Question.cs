using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionName;
    public List<Option> options;
    public int points;

    public Question(string questionName, List<Option> options, int points)
    {
        this.questionName = questionName;
        this.options = options;
        this.points = points;
    }
}
