using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Navigation", menuName = "Navigation Data")]
public class Navigation : ScriptableObject
{
    public int universeSelected;

    public int solarSystemSelected;
    public int planetSelected;
}
