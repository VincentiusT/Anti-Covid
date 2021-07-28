using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pharmacy Level System", menuName = "LevelSystem/Create Pharmacy Level System")]
public class PharmacyLevelSystem : ScriptableObject
{
    public string sellWhat = "";
    public int price = 0;
    public int[] prices;
    public int transmissionDecreaseRate=0;
    public Sprite sprite = null;
}
