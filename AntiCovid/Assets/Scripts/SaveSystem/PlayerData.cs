using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public LevelData levelData;

    public PlayerData(Inventory inventory)
    {
        levelData = inventory.levelData;
    }
}
