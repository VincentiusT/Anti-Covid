using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Citizen : MonoBehaviour
{
    public static Citizen instance;

    [SerializeField] private int totalCitizen = 1000;
    private int sickPeoples = 0;
    private int healthyPeoples;
    private int hospitalizedPeoples;
    private int vaksinedPeoples;
    private int vaksinedPeoples2;
    private int unvaccinatedPeoples;
    private int unvaccinatedPeoples2;
    private int deadPeoples;
    private float deadPeopleLimit = 0.5f; // percentage from totalCitizen

    [SerializeField] private int transmissionRate = 10; //people per second

    private int GetVirusRateAfterFirstVaccine = 2000; // random 1-20 kalo kena angka 1 kena virus

    [SerializeField] private float timeToIncreaseTransmissionRate = 20;
    private float timeTemp;
    [SerializeField] private int transmissionIncreaseRate = 25;

    [SerializeField] [Range(0, 1f)] private float deathRate = 0.03f; //percentage from sickpeople
    private float timeUntilDeath = 30f;
    private float timeUntilDeathTemp;

    private float crowdSpawnTimeMax = 30f;
    private float crowdSpawnTimeMin = 8f;
    private float crowdSpawnTime;

    [SerializeField] [Range(0, 1f)] private float awareness = 0.1f; //percentage

    public List<Crowd> crowds;
    [SerializeField] private GameObject[] crowdObj;
    [SerializeField] private Transform[] crowdSpawnPoint;
  
    [SerializeField] private TextMeshProUGUI sickPeopleText;
    [SerializeField] private TextMeshProUGUI healthyPeopleText;
    [SerializeField] private TextMeshProUGUI transmissionRateText;
    [SerializeField] private TextMeshProUGUI hospitalizedPeopleText;
    [SerializeField] private TextMeshProUGUI deadPeopleText;
    [SerializeField] private TextMeshProUGUI vaccinatedPeopleText;
    [SerializeField] private TextMeshProUGUI vaccinatedPeople2Text;
    [SerializeField] private TextMeshProUGUI citizenAliveText;
    [SerializeField] private TextMeshProUGUI deathRateText;
    [SerializeField] private TextMeshProUGUI transmissionIncreaseRateText;
    [SerializeField] private TextMeshProUGUI AwarenessText;
    [SerializeField] private TextMeshProUGUI unvaccinatedPeopleText;
    [SerializeField] private TextMeshProUGUI unvaccinatedPeople2Text;

    float second=1f;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        crowds = new List<Crowd>();
        crowdSpawnTime = Random.Range(crowdSpawnTimeMin, crowdSpawnTimeMax);
        crowdSpawnTime -= crowdSpawnTime * (1 - awareness);
        timeTemp = timeToIncreaseTransmissionRate;
        timeUntilDeathTemp = timeUntilDeath;
        unvaccinatedPeoples = totalCitizen;
        unvaccinatedPeoples2 = totalCitizen;
        healthyPeoples = totalCitizen;
    }
    private void Update()
    {
        UpdateUIText();

        //win codition
        if(vaksinedPeoples2 >= totalCitizen * 0.75)
        {
            if (AudioManager.instance != null) AudioManager.instance.Play("win");
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
            crowdSpawnTime += crowdSpawnTime * awareness;
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
        if(sickPeoples<10) sickPeopleText.text = sickPeoples.ToString("0");
        else sickPeopleText.text = sickPeoples.ToString("#,#");

        if(healthyPeoples<10) healthyPeopleText.text = healthyPeoples.ToString("0");
        else healthyPeopleText.text = healthyPeoples.ToString("#,#");
        
        if(transmissionRate <10) transmissionRateText.text = transmissionRate.ToString("0");
        else transmissionRateText.text = transmissionRate.ToString("#,#");

        if(HospitalManager.instance.GetAllHospitalizePeople() <10) hospitalizedPeopleText.text = HospitalManager.instance.GetAllHospitalizePeople().ToString("0");
        else hospitalizedPeopleText.text = HospitalManager.instance.GetAllHospitalizePeople().ToString("#,#");

        if(vaksinedPeoples<10) vaccinatedPeopleText.text = vaksinedPeoples.ToString("0");
        else vaccinatedPeopleText.text = vaksinedPeoples.ToString("#,#");

        if(deadPeoples <10) deadPeopleText.text = deadPeoples.ToString("0");
        else deadPeopleText.text = deadPeoples.ToString("#,#");

        if(totalCitizen<10) citizenAliveText.text = totalCitizen.ToString("0");
        else citizenAliveText.text = totalCitizen.ToString("#,#");

        deathRateText.text = (deathRate*100).ToString();
        transmissionIncreaseRateText.text = transmissionIncreaseRate.ToString("#,#");
        AwarenessText.text = (awareness*100).ToString(".0"); 

        if(unvaccinatedPeoples<10) unvaccinatedPeopleText.text = unvaccinatedPeoples.ToString("0");
        else unvaccinatedPeopleText.text = unvaccinatedPeoples.ToString("#,#");

        if(vaksinedPeoples2<10) vaccinatedPeople2Text.text = vaksinedPeoples2.ToString("0");
        else vaccinatedPeople2Text.text = vaksinedPeoples2.ToString("#,#");

        if(unvaccinatedPeoples2<10) unvaccinatedPeople2Text.text = unvaccinatedPeoples2.ToString("0");
        else unvaccinatedPeople2Text.text = unvaccinatedPeoples2.ToString("#,#");
    }
    public void GetVirus(int total)
    {
        //orang udah vaksin pertama bisa kena lagi
        if (vaksinedPeoples > 0)
        {
            Debug.Log("mampus kena");
            int randSickVaccinatedPeople = Random.Range(1, (int)(total * 0.5f));
            GetVirusAfterFirstVaccine(randSickVaccinatedPeople);
        }

        if (total > healthyPeoples)
        {
            sickPeoples += healthyPeoples;
            healthyPeoples = 0;
            return;
        }
        
        sickPeoples += total;

        //int healthyCount = totalCitizen - HospitalManager.instance.GetAllHospitalizePeople() - sickPeoples - vaksinedPeoples - vaksinedPeoples2;
        //if (healthyCount < 0) healthyPeoples = 0;
        //else healthyPeoples = healthyCount;

        healthyPeoples -= total;
    }

    public void GetVirusAfterFirstVaccine(int total)
    {
        int rate = Random.Range(0, GetVirusRateAfterFirstVaccine);
        if(rate == 1)
        {
            if (vaksinedPeoples < total)
            {
                sickPeoples += vaksinedPeoples;
                unvaccinatedPeoples += vaksinedPeoples;
                vaksinedPeoples = 0;
                return;
            }
            sickPeoples += total;
            vaksinedPeoples -= total;
            unvaccinatedPeoples += total;
        }
    }

    public void GetFirstVaccine(int people)
    {
        if (healthyPeoples < 1) return;

        if(healthyPeoples < people)
        {
            vaksinedPeoples += healthyPeoples;
            unvaccinatedPeoples -= healthyPeoples;
            healthyPeoples = 0;
        }
        else
        {
            vaksinedPeoples += people;
            healthyPeoples -= people;
            unvaccinatedPeoples -= people;
        }
    }

    public void GetSeccondVaccine(int people)
    {
        if (vaksinedPeoples < 1) return;

        if(vaksinedPeoples < people)
        {
            vaksinedPeoples2 += vaksinedPeoples;
            unvaccinatedPeoples2 -= vaksinedPeoples;
            vaksinedPeoples = 0;
        }
        else
        {
            vaksinedPeoples2 += people;
            vaksinedPeoples -= people;
            unvaccinatedPeoples2 -= people;
        }
    }

    public void Dead(int total)
    {
        if (sickPeoples < total)
        {
            deadPeoples += sickPeoples;
            totalCitizen -= sickPeoples;
            unvaccinatedPeoples -= sickPeoples;
            unvaccinatedPeoples2 -= sickPeoples;
            sickPeoples = 0;
            return;
        }
        deadPeoples += total;

        sickPeoples -= total;
        totalCitizen -= total;
        unvaccinatedPeoples -= total;
        unvaccinatedPeoples2 -= total;
    }

    public void SpawnCrowd()
    {
        int limit =0;
        int idx = Random.Range(0, crowdObj.Length);

        if (PlayerPrefs.GetInt("crowdTutorial") == 0)
            idx = 0;

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

    public int VaksinedPeoples2
    {
        set { vaksinedPeoples2 = value; }
        get { return vaksinedPeoples2; }
    }

    public float Awareness
    {
        get { return awareness; }
        set { awareness = value; }
    }

    public int UnvaccinatedPeoples
    {
        get { return unvaccinatedPeoples; }
        set { unvaccinatedPeoples = value; }
    }
    public int UnvaccinatedPeoples2
    {
        get { return unvaccinatedPeoples2; }
        set { unvaccinatedPeoples2 = value; }
    }
}
