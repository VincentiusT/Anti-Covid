using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI sickPeopleText;
    [SerializeField] private TextMeshProUGUI healthyPeopleText;
    [SerializeField] private TextMeshProUGUI transmissionRateText;
    [SerializeField] private TextMeshProUGUI hospitalizedPeopleText;

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
        UpdateUIText();

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
            
            Debug.Log("org kena : " + sickPeoples + " || orang di rmh sakit:  "+ HospitalManager.instance.GetAllHospitalizePeople() + " || org sehat : " + healthyPeoples);
        }
    }

    void UpdateUIText()
    {
        sickPeopleText.text = sickPeoples.ToString("0");
        healthyPeopleText.text = healthyPeoples.ToString("0");
        transmissionRateText.text = transmissionRate.ToString("0");
        hospitalizedPeopleText.text = HospitalManager.instance.GetAllHospitalizePeople().ToString("0");
    }
    public void GetVirus(int total)
    {
        for(int i = 0; i < total; i++)
        {
            sickPeoples++;
        }
        healthyPeoples = totalCitizen - HospitalManager.instance.GetAllHospitalizePeople() - sickPeoples;
        
    }

    public int TransmissionRate
    {
        set { transmissionRate = value; }
        get { return transmissionRate; }
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
    public int VaksinedPeoples
    {
        set { vaksinedPeoples = value; }
        get { return vaksinedPeoples; }
    }
}
