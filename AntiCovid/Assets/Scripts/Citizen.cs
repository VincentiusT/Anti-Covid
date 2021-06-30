using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public static Citizen instance;

    private int totalCitizen = 1000000;
    private int sickPeoples = 100;
    private int healthyPeoples;
    private int hospitalizedPeoples;
    private int vaksinedPeoples;
    private int deadPeoples;

    private int transmissionRate = 10; //people per second

    private float timeToIncreaseTransmissionRate = 20;
    private float timeTemp;
    private int increaseRate = 10;

    float second=1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeTemp = timeToIncreaseTransmissionRate;
    }
    private void Update()
    {
        if (timeToIncreaseTransmissionRate <= 0)
        {
            transmissionRate += increaseRate;
            timeToIncreaseTransmissionRate = timeTemp;
        }
        else
        {
            timeToIncreaseTransmissionRate -= Time.deltaTime;
        }

        if (second <= 0)
        {
            //orang kena virus
            GetVirus(transmissionRate);
            second = 1f;
        }
        else
        {
            second -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            healthyPeoples = totalCitizen - HospitalManager.instance.GetAllHospitalizePeople() - sickPeoples;
            Debug.Log("org kena : " + sickPeoples + " || orang di rmh sakit:  "+ HospitalManager.instance.GetAllHospitalizePeople() + " || org sehat : " + healthyPeoples);
        }
    }

    public void GetVirus(int total)
    {
        for(int i = 0; i < total; i++)
        {
            sickPeoples++;
        }
        healthyPeoples = totalCitizen - HospitalManager.instance.GetAllHospitalizePeople() - sickPeoples;
        
    }

    public int TotalCitizen
    {
        set { totalCitizen = value; }
        get { return totalCitizen; }
    }

    public int HealthyPeoples
    {
        set { healthyPeoples = value; }
        get { return healthyPeoples; }
    }

    public int SickPeoples
    {
        set { sickPeoples = value; }
        get { return sickPeoples; }
    }

    public int HospitalizedPeoples
    {
        set { hospitalizedPeoples = value; }
        get { return hospitalizedPeoples; }
    }
}
