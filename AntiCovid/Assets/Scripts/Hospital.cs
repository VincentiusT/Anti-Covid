using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hospital : MonoBehaviour
{
    private int level=1;
    private int capacity = 100;
    [SerializeField] private Slider slider;
    [SerializeField] private HospitalLevelSystem[] hospitalLevelSystem;
    private int upgradePrice;

    private int hospitalizedPeoples;
    private int releaseCount = 5; //berapa orang yang keluar dari rumah sakit

    private float restTime = 20f; //berapa waktu yg dibutuhin sebelom orang keluar dari rumah sakit
    private float restTimeOriginal;

    private void Awake()
    {
        restTimeOriginal = restTime;
        slider.maxValue = capacity;
        upgradePrice = hospitalLevelSystem[1].price;
    }

    private void Update()
    {
        if(restTime <= 0 && hospitalizedPeoples > 0)
        {
            ReleaseHealthyPeople();
            restTime = restTimeOriginal;
        }
        else
        {
            restTime -= Time.deltaTime;
        }

        updateSlider();
    }

    public void ReceiveSickPeople(int peoples)
    {
        if (hospitalizedPeoples >= capacity)
        {
            Debug.Log("Hospital " + name + " is full!");
            return;
        }

        hospitalizedPeoples+= peoples;
        Citizen.instance.SickPeoples-= peoples;

        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;
        Debug.Log(name + "  " + hospitalizedPeoples);
    }

    public void ReleaseHealthyPeople()
    {
        if (hospitalizedPeoples < releaseCount)
        {
            Citizen.instance.HealthyPeoples += hospitalizedPeoples;
            Debug.Log("release from " + name + "  " + hospitalizedPeoples);
            hospitalizedPeoples = 0;
        }
        else
        {
            hospitalizedPeoples -= releaseCount;
            Citizen.instance.HealthyPeoples += releaseCount;
            Debug.Log("release from " + name + "  " + releaseCount);
        }

        if (Citizen.instance.HealthyPeoples >= Citizen.instance.TotalCitizen)
        {
            Citizen.instance.HealthyPeoples = Citizen.instance.TotalCitizen;
        }
        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;
        
        
    }

    public void UpgradeHospital()
    {
        level++;

        capacity = hospitalLevelSystem[level - 1].capacity;
        releaseCount = hospitalLevelSystem[level - 1].outRate;
        restTime = hospitalLevelSystem[level - 1].outSpeed;
        slider.maxValue = capacity;

        if (level >= hospitalLevelSystem.Length) return;
        upgradePrice = hospitalLevelSystem[level].price;
    }

    public bool CheckMaxLevel()
    {
        return level >= hospitalLevelSystem.Length;
    }

    private void updateSlider()
    {
        slider.value = hospitalizedPeoples;
    }

    public int GetHospitalizePeople()
    {
        return hospitalizedPeoples;
    }

    public int Level
    {
        set { level = value; }
        get { return level; }
    }

    public int UpgradePrice
    {
        get { return upgradePrice; }
    }
    public int Capacity
    {
        get { return capacity; }
    }
    public float RestTime
    {
        get { return restTime; }
    }
    public int ReleaseCount
    {
        get { return releaseCount; }
    }
}
