using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public int coin;
    public int kaki = 0, tangan = 0, mata = 0, pieceHati = 0;
    public int biji = 0, batu = 0;

    public PlayerData(Inventory inventory)
    {
        coin = inventory.coin;
        kaki = inventory.kaki;
        tangan = inventory.tangan;
        mata = inventory.mata;
        pieceHati = inventory.pieceHati;
        biji = inventory.biji;
        batu = inventory.batu;
    }
}
