using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private bool load = true;

    public LevelData levelData;

    [SerializeField] DayManager dayManager;

    private void Awake()
    {
        //if (load)
        //{
        //    Load();
        //    load = false;
        //}
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        levelData = GetLevelDataNow();
        SaveSystem.SavePlayer(instance);
    }

    private LevelData GetLevelDataNow()
    {
        LevelData thisLevelData = new LevelData();
        thisLevelData.Day = dayManager.getDay();
        thisLevelData.Money = Goverment.instance.Money;
        thisLevelData.TotalCitizen = Citizen.instance.TotalCitizen;
        thisLevelData.TransmissionRate = Citizen.instance.TransmissionRateTotal;
        thisLevelData.SickPeoples = Citizen.instance.SickPeoples;
        thisLevelData.HealthyPeoples = Citizen.instance.HealthyPeoples;
        thisLevelData.HospitalizedPeoples = Citizen.instance.HospitalizedPeoples;
        thisLevelData.VaksinedPeoples = Citizen.instance.VaksinedPeoples;
        thisLevelData.VaksinedPeoples2 = Citizen.instance.VaksinedPeoples2;
        thisLevelData.UnvaccinatedPeoples = Citizen.instance.UnvaccinatedPeoples;
        thisLevelData.UnvaccinatedPeoples2 = Citizen.instance.UnvaccinatedPeoples2;
        thisLevelData.DeadPeoples = Citizen.instance.DeadPeoples;
        thisLevelData.TransmissionIncreaseRate = Citizen.instance.TransmissionIncreaseRate;
        thisLevelData.Awareness = Citizen.instance.Awareness;

        return thisLevelData;
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            return;
        }

        levelData = data.levelData;

        Debug.Log(levelData.Money);
        dayManager.setDay(levelData.Day);
        Goverment.instance.Money = levelData.Money;
        Citizen.instance.TotalCitizen = levelData.TotalCitizen;
        Citizen.instance.TransmissionRateTotal = levelData.TransmissionRate;
        Citizen.instance.SickPeoples = levelData.SickPeoples;
        Citizen.instance.HealthyPeoples = levelData.HealthyPeoples;
        Citizen.instance.HospitalizedPeoples = levelData.HospitalizedPeoples;
        Citizen.instance.VaksinedPeoples = levelData.VaksinedPeoples;
        Citizen.instance.VaksinedPeoples2 = levelData.VaksinedPeoples2;
        Citizen.instance.UnvaccinatedPeoples = levelData.UnvaccinatedPeoples;
        Citizen.instance.UnvaccinatedPeoples2 = levelData.UnvaccinatedPeoples2;
        Citizen.instance.DeadPeoples = levelData.DeadPeoples;
        Citizen.instance.TransmissionIncreaseRate = levelData.TransmissionIncreaseRate;
        Citizen.instance.Awareness = levelData.Awareness;
    }
}
