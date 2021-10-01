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
        if(instance == null)
        {
            Debug.Log("set instance");
            instance = this;
        }

        levelData = new LevelData[SceneManager.sceneCountInBuildSettings - 2];
    }

    private void Start()
    {
        if (load)
        {
            Invoke("Load", 0.1f);
            load = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("save");
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log("load");
            Load();
        }
    }

    public void Save(bool isResetWin = false)
    {
        if (isResetWin)
            levelData[GetSceneIndex()] = null;
        else
            levelData[GetSceneIndex()] = GetLevelDataNow();
        SaveSystem.SavePlayer(this);
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
        thisLevelData.VaccineStock = VaksinManager.instance.VaccineStock;

        if (HospitalManager.instance != null)
            System.Array.ForEach(HospitalManager.instance.hospitals, h => { thisLevelData.hospitalDatas.Add(h != null? h.hospitalData : null); });
        if (AmbulanceManager.instance != null)
            System.Array.ForEach(AmbulanceManager.instance.ambulances, a => { thisLevelData.ambulanceDatas.Add(a != null ? a.ambulanceData : null); });
        if (PharmacyManager.instance != null)
            System.Array.ForEach(PharmacyManager.instance.pharmacy, p => { thisLevelData.pharmacyDatas.Add(p != null ? p.pharmacyData : null); });
        if (VaksinManager.instance != null)
            System.Array.ForEach(VaksinManager.instance.vaksinPlace, v => { thisLevelData.vaccinePlaceDatas.Add(v != null ? v.vaccinePlaceData : null); });
        if (OfficerManager.instance != null)
            thisLevelData.officerData = OfficerManager.instance.officer.officerData;

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

        //Debug.Log(thisLevelData.Money);
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
        VaksinManager.instance.VaccineStock = thisLevelData.VaccineStock;
        RebuildAllBuilding(thisLevelData);
        //Debug.Log(thisLevelData.hospitalDatas[0].capacity);
    }

    private void RebuildAllBuilding(LevelData data)
    {
        int maxBuildingCount = GetMostBuilding(data);

        for (int i = 0; i < maxBuildingCount; i++)
        {
            if (i < data.hospitalDatas.Count)
                HospitalManager.instance.RebuildHospital(i, data.hospitalDatas[i] != null ? data.hospitalDatas[i].level : 0);
            if (i < data.ambulanceDatas.Count)
                AmbulanceManager.instance.RebuildAmbulance(i, data.ambulanceDatas[i] != null ? data.ambulanceDatas[i].level : 0);
            if (i < data.pharmacyDatas.Count)
                PharmacyManager.instance.RebuildPharmacy(i, data.pharmacyDatas[i] != null ? data.pharmacyDatas[i].level : 0);
            if (i < data.vaccinePlaceDatas.Count)
                VaksinManager.instance.RebuildVaccinePlace(i, data.vaccinePlaceDatas[i] != null ? data.vaccinePlaceDatas[i].level : 0);
        }
        if(OfficerManager.instance != null)
            OfficerManager.instance.RebuildOfficerPlace(data.officerData != null ? data.officerData.level : 0);
    }

    private int GetMostBuilding(LevelData data)
    {
        int[] buildingsCount = { data.ambulanceDatas.Count, data.hospitalDatas.Count, data.pharmacyDatas.Count, data.vaccinePlaceDatas.Count };
        return Mathf.Max(buildingsCount);
    }

    private int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex - 2;
    }
}
