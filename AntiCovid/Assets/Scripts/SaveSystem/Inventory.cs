using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private bool load = true;

    public LevelData[] levelData;

    [SerializeField] DayManager dayManager;

    private void Awake()
    {
        levelData = new LevelData[SceneManager.sceneCountInBuildSettings - 2];
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
            Debug.Log("save");
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("load");
            Load();
        }
    }

    public void Save()
    {
        levelData[GetSceneIndex()] = GetLevelDataNow();
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
        //thisLevelData.Hospitals = HospitalManager.instance.Hospitals;

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
        LevelData thisLevelData = levelData[GetSceneIndex()];
        if (thisLevelData == null) return;

        Debug.Log(thisLevelData.Money);
        dayManager.setDay(thisLevelData.Day);
        Goverment.instance.Money = thisLevelData.Money;
        Citizen.instance.TotalCitizen = thisLevelData.TotalCitizen;
        Citizen.instance.TransmissionRateTotal = thisLevelData.TransmissionRate;
        Citizen.instance.SickPeoples = thisLevelData.SickPeoples;
        Citizen.instance.HealthyPeoples = thisLevelData.HealthyPeoples;
        Citizen.instance.HospitalizedPeoples = thisLevelData.HospitalizedPeoples;
        Citizen.instance.VaksinedPeoples = thisLevelData.VaksinedPeoples;
        Citizen.instance.VaksinedPeoples2 = thisLevelData.VaksinedPeoples2;
        Citizen.instance.UnvaccinatedPeoples = thisLevelData.UnvaccinatedPeoples;
        Citizen.instance.UnvaccinatedPeoples2 = thisLevelData.UnvaccinatedPeoples2;
        Citizen.instance.DeadPeoples = thisLevelData.DeadPeoples;
        Citizen.instance.TransmissionIncreaseRate = thisLevelData.TransmissionIncreaseRate;
        Citizen.instance.Awareness = thisLevelData.Awareness;
    }

    private int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex - 2;
    }
}
