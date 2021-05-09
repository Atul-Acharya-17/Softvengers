using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player Data")]
public class Player : ScriptableObject
{
    public int universePogress;
    public int solarSystemProgress;
    public int planetProgress;
    public int colorChoice;
    public double totalScore;
}
