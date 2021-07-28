using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hospital Level System", menuName = "LevelSystem/Create Hospital Level System")]
public class HospitalLevelSystem : ScriptableObject
{
    public int level = 0;
    public int price = 0;
    public int capacity = 0;
    public int outRate = 0;
    public int outSpeed = 0;
    public int peopleOutPerTap = 0;
    public Sprite sprite = null;
}
