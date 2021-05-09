using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public string option;
    public bool isCorrect;

    public Option(string option, bool isCorrect)
    {
        this.option = option;
        this.isCorrect = isCorrect;
    }

}
