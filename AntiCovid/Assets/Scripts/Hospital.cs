using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hospital : MonoBehaviour
{
    public HospitalData hospitalData;

    [SerializeField]private GameObject hospitalEmptyMark;
    //private int level=1;
    //private int capacity = 100;
    [SerializeField] private Slider slider;
    [SerializeField] private HospitalLevelSystem[] hospitalLevelSystem;
    [SerializeField] private GameObject peopleIn;
    [SerializeField] private GameObject peopleOut;
    private int upgradePrice;

    private SpriteRenderer sprite;

    private int hospitalizedPeoples;
    //private int releaseCount = 5; //berapa orang yang keluar dari rumah sakit

    //private float restTime = 20f; //berapa waktu yg dibutuhin sebelom orang keluar dari rumah sakit
    private float restTimeOriginal;

    //private int peopleOutPerTap;

    private void Awake()
    {
        hospitalData.level = 1;
        sprite = GetComponent<SpriteRenderer>();
        hospitalData.releaseCount = hospitalLevelSystem[0].outRate;
        hospitalData.restTime = hospitalLevelSystem[0].outSpeed;
        hospitalData.capacity = hospitalLevelSystem[0].capacity;
        hospitalData.peopleOutPerTap = hospitalLevelSystem[0].peopleOutPerTap;
        sprite.sprite = hospitalLevelSystem[0].sprite;
        restTimeOriginal = hospitalData.restTime;
        slider.maxValue = hospitalData.capacity;
        upgradePrice = hospitalLevelSystem[1].price;
    }

    public void AssignLevelSystem(HospitalLevelSystem[] lvl)
    {
        hospitalLevelSystem = lvl;
        hospitalData.releaseCount = hospitalLevelSystem[0].outRate;
        hospitalData.restTime = hospitalLevelSystem[0].outSpeed;
        hospitalData.capacity = hospitalLevelSystem[0].capacity;
        hospitalData.peopleOutPerTap = hospitalLevelSystem[0].peopleOutPerTap;
        sprite.sprite = hospitalLevelSystem[0].sprite;
        restTimeOriginal = hospitalData.restTime;
        slider.maxValue = hospitalData.capacity;
        upgradePrice = hospitalLevelSystem[1].price;
    }
    private void Update()
    {
        if(hospitalData.restTime <= 0 && hospitalizedPeoples > 0)
        {
            ReleaseHealthyPeople();
           hospitalData.restTime = restTimeOriginal;
        }
        else
        {
            hospitalData.restTime -= Time.deltaTime;
        }
        if (hospitalizedPeoples <= 0)
        {
            hospitalEmptyMark.SetActive(true);
        }
        else
        {
            hospitalEmptyMark.SetActive(false);
        }
        updateSlider();
    }


    public void ReceiveSickPeople(int peoples)
    {
        if(Citizen.instance.SickPeoples < 1)
        {
            return;
        }
        if (hospitalizedPeoples >= hospitalData.capacity)
        {
            Debug.Log("Hospital " + name + " is full!");
            return;
        }

        if(hospitalizedPeoples+peoples >= hospitalData.capacity)
        {
            hospitalizedPeoples = hospitalData.capacity;
            Citizen.instance.SickPeoples -= hospitalizedPeoples + peoples - hospitalData.capacity;
        }
        else
        {
            if (Citizen.instance.SickPeoples - peoples <= 0)
            {
                hospitalizedPeoples += Citizen.instance.SickPeoples;
                Citizen.instance.SickPeoples = 0;
            }
            else
            {
                hospitalizedPeoples += peoples;
                Citizen.instance.SickPeoples-= peoples;
            }
        }

        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;

        ShowPeopleText(peoples);

        Debug.Log(name + "  " + hospitalizedPeoples);
    }

    public void ReleaseHealthyPeople()
    {
        if (hospitalizedPeoples < hospitalData.releaseCount)
        {
            Citizen.instance.HealthyPeoples += hospitalizedPeoples;
            //Citizen.instance.UnvaccinatedPeoples += hospitalizedPeoples;
            //Citizen.instance.UnvaccinatedPeoples2 += hospitalizedPeoples;
            Debug.Log("release from " + name + "  " + hospitalizedPeoples);
            ShowPeopleText(-hospitalizedPeoples);
            hospitalizedPeoples = 0;
        }
        else
        {
            hospitalizedPeoples -= hospitalData.releaseCount;
            Citizen.instance.HealthyPeoples += hospitalData.releaseCount;
            //Citizen.instance.UnvaccinatedPeoples += releaseCount;
            //Citizen.instance.UnvaccinatedPeoples2 += releaseCount;
            Debug.Log("release from " + name + "  " + hospitalData.releaseCount);
            ShowPeopleText(-hospitalData.releaseCount);
        }

        if (Citizen.instance.HealthyPeoples >= Citizen.instance.TotalCitizen)
        {
            Citizen.instance.HealthyPeoples = Citizen.instance.TotalCitizen;
        }
        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;
        

    }

    private void ShowPeopleText(int total)
    {
        int x = Random.Range(-2, 2);
        int y = Random.Range(-2, 2);
        Vector3 offset = new Vector3(x/10f, y/10f, 0);
        if (total > 0)
        {
            GameObject go = Instantiate(peopleIn, transform.position + offset, transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + total;
            Destroy(go, 0.5f);
        }
        else
        {
            GameObject go = Instantiate(peopleOut, transform.position + offset, transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
            Destroy(go, 0.5f);
        }
    }



    public void UpgradeHospital()
    {
        hospitalData.level++;

        hospitalData.capacity = hospitalLevelSystem[hospitalData.level - 1].capacity;
        hospitalData.releaseCount = hospitalLevelSystem[hospitalData.level - 1].outRate;
        hospitalData.restTime = hospitalLevelSystem[hospitalData.level - 1].outSpeed;
        hospitalData.peopleOutPerTap = hospitalLevelSystem[hospitalData.level - 1].peopleOutPerTap;
        sprite.sprite = hospitalLevelSystem[hospitalData.level -1].sprite;
        slider.maxValue = hospitalData.capacity;
        restTimeOriginal = hospitalData.restTime;
        if (hospitalData.level >= hospitalLevelSystem.Length) return;
        upgradePrice = hospitalLevelSystem[hospitalData.level].price;
    }

    public bool CheckMaxLevel()
    {
        return hospitalData.level >= hospitalLevelSystem.Length;
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
        set { hospitalData.level = value; }
        get { return hospitalData.level; }
    }

    public int UpgradePrice
    {
        get { return upgradePrice; }
    }
    public int Capacity
    {
        get { return hospitalData.capacity; }
    }
    public float RestTime
    {
        get { return hospitalData.restTime; }
    }
    public int ReleaseCount
    {
        get { return hospitalData.releaseCount; }
    }
    
    public int PeopleOutPerTap
    {
        get { return hospitalData.peopleOutPerTap; }
    }

    public Sprite GetSprite()
    {
        return sprite.sprite;
    }

    public HospitalLevelSystem GetNextValue(int x)
    {
        return hospitalLevelSystem[x];
    }
}
