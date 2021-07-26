using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ambulance Level System", menuName = "LevelSystem/Create Ambulance Level System")]
public class AmbulanceLevelSystem : ScriptableObject
{
    public int level = 0;
    public int price = 0;
    public int pickupRate = 0;
    public int pickupTime = 0;
    public int pickupTimeMax = 0;
    //public Sprite sprite = null;
}
