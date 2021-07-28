using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Officer Level System", menuName = "LevelSystem/Create Officer Level System")]
public class OfficerLevelSystem : ScriptableObject
{
    public int level = 0;
    public int price = 0;
    public int refillTime = 0;
    public Sprite sprite = null;
}
