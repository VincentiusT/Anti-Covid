using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hospital : MonoBehaviour
{
    private int level=1;
    private int capacity = 100;
    [SerializeField] private Slider slider;
    [SerializeField] private HospitalLevelSystem[] hospitalLevelSystem;
    [SerializeField] private GameObject peopleIn;
    [SerializeField] private GameObject peopleOut;
    private int upgradePrice;

    private SpriteRenderer sprite;

    private int hospitalizedPeoples;
    private int releaseCount = 5; //berapa orang yang keluar dari rumah sakit

    private float restTime = 20f; //berapa waktu yg dibutuhin sebelom orang keluar dari rumah sakit
    private float restTimeOriginal;

    private int peopleOutPerTap;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        releaseCount = hospitalLevelSystem[0].outRate;
        restTime = hospitalLevelSystem[0].outSpeed;
        capacity = hospitalLevelSystem[0].capacity;
        peopleOutPerTap = hospitalLevelSystem[0].peopleOutPerTap;
        sprite.sprite = hospitalLevelSystem[0].sprite;
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
        if(Citizen.instance.SickPeoples < 1)
        {
            return;
        }
        if (hospitalizedPeoples >= capacity)
        {
            Debug.Log("Hospital " + name + " is full!");
            return;
        }

        if(hospitalizedPeoples+peoples >= capacity)
        {
            hospitalizedPeoples = capacity;
            Citizen.instance.SickPeoples -= hospitalizedPeoples + peoples - capacity;
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
        if (hospitalizedPeoples < releaseCount)
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
            hospitalizedPeoples -= releaseCount;
            Citizen.instance.HealthyPeoples += releaseCount;
            //Citizen.instance.UnvaccinatedPeoples += releaseCount;
            //Citizen.instance.UnvaccinatedPeoples2 += releaseCount;
            Debug.Log("release from " + name + "  " + releaseCount);
            ShowPeopleText(-releaseCount);
        }

        if (Citizen.instance.HealthyPeoples >= Citizen.instance.TotalCitizen)
        {
            Citizen.instance.HealthyPeoples = Citizen.instance.TotalCitizen;
        }
        Citizen.instance.HospitalizedPeoples = hospitalizedPeoples;
        

    }

    private void ShowPeopleText(int total)
    {
        if (total > 0)
        {
            GameObject go = Instantiate(peopleIn, transform.position, transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + total;
            Destroy(go, 0.5f);
        }
        else
        {
            GameObject go = Instantiate(peopleOut, transform.position, transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + total;
            Destroy(go, 0.5f);
        }
    }



    public void UpgradeHospital()
    {
        level++;

        capacity = hospitalLevelSystem[level - 1].capacity;
        releaseCount = hospitalLevelSystem[level - 1].outRate;
        restTime = hospitalLevelSystem[level - 1].outSpeed;
        peopleOutPerTap = hospitalLevelSystem[level - 1].peopleOutPerTap;
        sprite.sprite = hospitalLevelSystem[level -1].sprite;
        slider.maxValue = capacity;
        restTimeOriginal = restTime;
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
    
    public int PeopleOutPerTap
    {
        get { return peopleOutPerTap; }
    }

    public Sprite GetSprite()
    {
        return sprite.sprite;
    }
}
