using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hospital : MonoBehaviour
{
    private int capacity = 100;
    [SerializeField] private Slider slider;
    private int upgradePrice;

    private int hospitalizedPeoples;
    private int releaseCount = 5; //berapa orang yang keluar dari rumah sakit

    private float restTime = 20f; //berapa waktu yg dibutuhin sebelom orang keluar dari rumah sakit
    private float restTimeOriginal;

    private void Start()
    {
        restTimeOriginal = restTime;
        slider.maxValue = capacity;
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

    private void updateSlider()
    {
        slider.value = hospitalizedPeoples;
    }

    public int GetHospitalizePeople()
    {
        return hospitalizedPeoples;
    }
}
