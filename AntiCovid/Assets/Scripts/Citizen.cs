using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    private int totalCitizen = 1000000;
    private int sickPeoples;
    private int healthyPeople;

    private int transmissionRate = 10; //people per second

    private float timeToIncreaseTransmissionRate = 20;
    private float timeTemp;
    private int increaseRate = 10;

    float second=1f;
    private void Start()
    {
        timeTemp = timeToIncreaseTransmissionRate;
    }
    private void Update()
    {
        if (timeToIncreaseTransmissionRate  <= 0)
        {
            transmissionRate += increaseRate;
            timeToIncreaseTransmissionRate = timeTemp;
        }
        else
        {
            timeToIncreaseTransmissionRate -= Time.deltaTime;
        }


        if(second <= 0)
        {
            //orang kena virus
            GetVirus(transmissionRate);
            second = 1f;
        }
        else
        {
            second -= Time.deltaTime;
        }
    }

    public void GetVirus(int total)
    {
        for(int i = 0; i < total; i++)
        {
            //SickPeople sp = new SickPeople();
            sickPeoples++;
        }
        healthyPeople = totalCitizen - sickPeoples;
        Debug.Log("org kena : " + sickPeoples + " || org sehat : " + healthyPeople);
    }
}
