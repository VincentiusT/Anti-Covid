using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vaccine Level System", menuName = "LevelSystem/Create Vaccine Level System")]
public class VaccineLevelSystem : ScriptableObject
{
    public int level = 0;
    public int price = 0;
    public int[] prices;
    public int vaksinRate = 0;
    public int vaksinTime = 0;
    //public Sprite sprite = null;
}
