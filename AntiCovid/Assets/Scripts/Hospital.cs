using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private int capacity = 50;
    private int buyPrice;
    private int upgradePrice;

    private int hospitalizedPeoples;
    private int releaseCount = 5; //berapa orang yang keluar dari rumah sakit

    private float restTime = 20f; //berapa waktu yg dibutuhin sebelom orang keluar dari rumah sakit
    private float restTimeOriginal;

    private void Start()
    {
        restTimeOriginal = restTime;
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
            hospitalizedPeoples = 0;
            Citizen.instance.HealthyPeoples += hospitalizedPeoples;
        }
        else
        {
            hospitalizedPeoples -= releaseCount;
            Citizen.instance.HealthyPeoples += releaseCount;
        }

        if (Citizen.instance.HealthyPeoples >= Citizen.instance.TotalCitizen)
        {
            Citizen.instance.HealthyPeoples = Citizen.instance.TotalCitizen;
        }
        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;
        Debug.Log("release from "+ name + "  " + hospitalizedPeoples);
        
    }

    public int GetHospitalizePeople()
    {
        return hospitalizedPeoples;
    }
}
