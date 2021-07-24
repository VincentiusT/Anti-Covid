using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Policies", menuName = "Policies/Create New Policies")]
public class PolicyData : ScriptableObject
{
    public string name;
    public int price;
    public float rate;
    public int duration;
}
