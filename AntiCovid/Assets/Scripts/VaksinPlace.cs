using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaksinPlace : MonoBehaviour
{
    private int level = 1;

    private int vaksinRate = 5; //people per vaksinTime

    [SerializeField] private VaccineLevelSystem[] vaccineLevelSystem;

    private float vaksinTime = 10;
    private float vaksinTimeTemp;

    private float startSeccondVaccineTime = 50f;
    private float startSeccondVaccineTimeTemp;
    private float seccondVaksinTime = 10;
    private float seccondVaksinTimeTemp;

    private bool startSeccondVaccine = true;

    private int upgradePrice;

    bool done;

    private void Awake()
    {
        upgradePrice = vaccineLevelSystem[1].price;
        vaksinRate = vaccineLevelSystem[0].vaksinRate;
        vaksinTime = vaccineLevelSystem[0].vaksinTime;
        seccondVaksinTime = vaccineLevelSystem[0].vaksinTime;
        vaksinTimeTemp = vaksinTime;
        seccondVaksinTimeTemp = seccondVaksinTime;
        startSeccondVaccineTimeTemp = startSeccondVaccineTime;
    }
    void Start()
    {

    }

    void Update()
    {
        if(vaksinTime <= 0)
        {
            FirstVaccine(vaksinRate);
            vaksinTime = vaksinTimeTemp;
        }
        else
        {
            vaksinTime -= Time.deltaTime;
        }

        if (Citizen.instance.VaksinedPeoples > 0 && !done)
        {
            startSeccondVaccineTime = startSeccondVaccineTimeTemp;
            startSeccondVaccine = true;
            done = true;
        }

        if (startSeccondVaccine)
        {
            if (startSeccondVaccineTime <= 0)
            {
                if (Citizen.instance.VaksinedPeoples <= 0)
                {
                    startSeccondVaccine = false;
                    done = false;
                    //startSeccondVaccineTime = startSeccondVaccineTimeTemp;
                }

                //start seccond vaccine
                if (seccondVaksinTime <= 0)
                {
                    SeccondVaccine(vaksinRate);
                    seccondVaksinTime = seccondVaksinTimeTemp;
                }
                else
                {
                    seccondVaksinTime -= Time.deltaTime;
                }
            }
            else
            {
                startSeccondVaccineTime -= Time.deltaTime;
            }
        }
    }

    public void FirstVaccine(int people)
    {
        if(VaksinManager.instance.VaccineStock < people)
        {
            Citizen.instance.GetFirstVaccine(VaksinManager.instance.VaccineStock);
            VaksinManager.instance.VaccineStock = 0;
        }
        Citizen.instance.GetFirstVaccine(people);
        VaksinManager.instance.VaccineStock -= people;

    }

    public void SeccondVaccine(int people)
    {
        if (VaksinManager.instance.VaccineStock < people)
        {
            Citizen.instance.GetSeccondVaccine(VaksinManager.instance.VaccineStock);
            VaksinManager.instance.VaccineStock = 0;
        }
        Citizen.instance.GetSeccondVaccine(people);
        VaksinManager.instance.VaccineStock -= people;

    }

    public void UpgradeVaksinPlace()
    {
        level++;

        vaksinRate = vaccineLevelSystem[level - 1].vaksinRate;
        vaksinTime = vaccineLevelSystem[level - 1].vaksinTime;
        seccondVaksinTime = vaccineLevelSystem[level - 1].vaksinTime;
        vaksinTimeTemp = vaksinTime;
        seccondVaksinTimeTemp = seccondVaksinTime;

        if (level >= vaccineLevelSystem.Length) return;
        upgradePrice = vaccineLevelSystem[level].price;
    }

    public bool CheckMaxLevel()
    {
        return level >= vaccineLevelSystem.Length;
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
    public int VaksinRate
    {
        get { return vaksinRate; }
    }
    public float VaksinTime
    {
        get { return vaksinTime; }
    }
}
