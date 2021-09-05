using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int coin = 0;
    public int kaki = 0, tangan = 0, mata = 0, pieceHati = 0;
    public int biji = 0, batu = 0;

    private bool load = true;

    private void Awake()
    {
        if (load)
        {
            Load();
            load = false;
        }
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void Save()
    {
        SaveSystem.SavePlayer(instance);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            return;
        }
        
        coin = data.coin;
        kaki = data.kaki;
        tangan = data.tangan;
        mata = data.mata; 
        pieceHati = data.pieceHati;
        biji = data.biji;
        batu = data.batu;
    }
}
