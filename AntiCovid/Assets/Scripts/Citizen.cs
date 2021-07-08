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
    private float deadPeopleLimit = 0.5f; // percentage from totalCitizen

    private int transmissionRate = 10; //people per second

    private float timeToIncreaseTransmissionRate = 20;
    private float timeTemp;
    private int transmissionIncreaseRate = 25;

    private float deathRate = 0.03f; //percentage from sickpeople
    private float timeUntilDeath = 30f;
    private float timeUntilDeathTemp;

    private float crowdSpawnTimeMax = 20f;
    private float crowdSpawnTimeMin = 8f;
    private float crowdSpawnTime;

    public List<Crowd> crowds;
    [SerializeField] private GameObject[] crowdObj;
    [SerializeField] private Transform[] crowdSpawnPoint;
  
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
        int diff = PlayerPrefs.GetInt("diff");
        if (diff == 0)
        {
           
        }
        else if (diff == 1)
        {
            transmissionRate = 100;
            transmissionIncreaseRate = 50;
            deathRate = 7;
        }
        else if(diff == 2)
        {
            transmissionRate = 500;
            transmissionIncreaseRate = 100;
            deathRate = 10;
        }
    }

    private void Start()
    {
        crowds = new List<Crowd>();
        crowdSpawnTime = Random.Range(crowdSpawnTimeMin, crowdSpawnTimeMax);
        timeTemp = timeToIncreaseTransmissionRate;
        timeUntilDeathTemp = timeUntilDeath;
    }
    private void Update()
    {
        UpdateUIText();

        //win codition
        if(vaksinedPeoples >= totalCitizen)
        {
            GameManager.instance.Win();
        }
        //lose condition
        if(deadPeoples >= totalCitizen * deadPeopleLimit)
        {
            GameManager.instance.GameOver();
        }

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
            float deadCount = deathRate * (float)sickPeoples;
            Dead((int)deadCount);
            timeUntilDeath = timeUntilDeathTemp;
        }
        else
        {
            timeUntilDeath -= Time.deltaTime;
        }
        
        if(crowdSpawnTime <= 0)
        {
            SpawnCrowd();
            crowdSpawnTime = Random.Range(crowdSpawnTimeMin, crowdSpawnTimeMax);
        }
        else
        {
            crowdSpawnTime -= Time.deltaTime;
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

    public void SpawnCrowd()
    {
        int limit =0;
        int idx = Random.Range(0, crowdObj.Length);
        while(crowdSpawnPoint[idx].childCount != 0)
        {
            idx++;
            if (idx >= crowdObj.Length) idx = 0;
            limit++;
            if (limit >= crowdObj.Length)
            {
                return;
            }
        }

        GameObject go = Instantiate(crowdObj[idx], transform.position, transform.rotation) as GameObject;
        crowds.Add(go.GetComponent<Crowd>());
        go.name = "crowd" + idx;
        go.transform.parent = crowdSpawnPoint[idx];
    }

    public int TransmissionRateTotal
    {
        set { transmissionRate = value; }
        get { return transmissionRate;  }
    }

    public int TransmissionIncreaseRate
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
