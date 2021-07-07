using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Citizen : MonoBehaviour
{
    public static Citizen instance;

    private int totalCitizen = 1000000;
    private int sickPeoples = 0;
    private int healthyPeoples;
    private int hospitalizedPeoples;
    private int vaksinedPeoples;
    private int deadPeoples;

    private int transmissionRate = 10; //people per second

    private float timeToIncreaseTransmissionRate = 20;
    private float timeTemp;
    private int transmissionIncreaseRate = 25;

    private int deathRate = 3; //people dead per timeuntildeath
    private float timeUntilDeath = 30f;
    private float timeUntilDeathTemp;

    [SerializeField] private TextMeshProUGUI sickPeopleText;
    [SerializeField] private TextMeshProUGUI healthyPeopleText;
    [SerializeField] private TextMeshProUGUI transmissionRateText;
    [SerializeField] private TextMeshProUGUI hospitalizedPeopleText;
    [SerializeField] private TextMeshProUGUI deadPeopleText;
    [SerializeField] private TextMeshProUGUI vaccinatedPeopleText;
    [SerializeField] private TextMeshProUGUI citizenAliveText;

    float second=1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeTemp = timeToIncreaseTransmissionRate;
        timeUntilDeathTemp = timeUntilDeath;
    }
    private void Update()
    {
        UpdateUIText();

        if (timeToIncreaseTransmissionRate <= 0)
        {
            transmissionRate += transmissionIncreaseRate;
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

        if(timeUntilDeath <= 0 && sickPeoples > 0)
        {
            Dead(deathRate);
            timeUntilDeath = timeUntilDeathTemp;
        }
        else
        {
            timeUntilDeath -= Time.deltaTime;
        }
        

        //debug
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
        vaccinatedPeopleText.text = vaksinedPeoples.ToString("0");
        deadPeopleText.text = deadPeoples.ToString("0");
        citizenAliveText.text = totalCitizen.ToString("0");
    }
    public void GetVirus(int total)
    {
        sickPeoples += total;
        healthyPeoples = totalCitizen - HospitalManager.instance.GetAllHospitalizePeople() - sickPeoples;
        
    }

    public void Dead(int total)
    {
        if(sickPeoples < total)
        {
            deadPeoples += sickPeoples;
            sickPeoples = 0;
            totalCitizen -= sickPeoples;
            return;
        }
        deadPeoples += total;
        sickPeoples -= total;
        totalCitizen -= total;

    }

    public int TransmissionRate
    {
        set { transmissionIncreaseRate = value; }
        get { return transmissionIncreaseRate; }
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
