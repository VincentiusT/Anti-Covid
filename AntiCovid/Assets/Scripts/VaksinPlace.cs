using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaksinPlace : MonoBehaviour
{

    public VaccinePlaceData vaccinePlaceData;
    //private int level = 1;

    //private int vaksinRate = 5; //people per vaksinTime

    [SerializeField] private VaccineLevelSystem[] vaccineLevelSystem;

    //private float vaksinTime = 10;
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
        vaccinePlaceData.level = 1;
    }

    public void AssignLevelSystem(VaccineLevelSystem[] lvl)
    {
        vaccineLevelSystem = lvl;
        upgradePrice = vaccineLevelSystem[1].price;
        vaccinePlaceData.vaksinRate = vaccineLevelSystem[0].vaksinRate;
        vaccinePlaceData.vaksinTime = vaccineLevelSystem[0].vaksinTime;
        seccondVaksinTime = vaccineLevelSystem[0].vaksinTime;
        vaksinTimeTemp = vaccinePlaceData.vaksinTime;
        seccondVaksinTimeTemp = seccondVaksinTime;
        startSeccondVaccineTimeTemp = startSeccondVaccineTime;
    }

    void Start()
    {

    }

    void Update()
    {
        if(vaccinePlaceData.vaksinTime <= 0)
        {
            FirstVaccine(vaccinePlaceData.vaksinRate);
            vaccinePlaceData.vaksinTime = vaksinTimeTemp;
        }
        else
        {
            vaccinePlaceData.vaksinTime -= Time.deltaTime;
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
                    SeccondVaccine(vaccinePlaceData.vaksinRate);
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
            return;
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
            return;
        }
        Citizen.instance.GetSeccondVaccine(people);
        VaksinManager.instance.VaccineStock -= people;

    }

    public void UpgradeVaksinPlace()
    {
        vaccinePlaceData.level++;

        vaccinePlaceData.vaksinRate = vaccineLevelSystem[vaccinePlaceData.level - 1].vaksinRate;
        vaccinePlaceData.vaksinTime = vaccineLevelSystem[vaccinePlaceData.level - 1].vaksinTime;
        seccondVaksinTime = vaccineLevelSystem[vaccinePlaceData.level - 1].vaksinTime;
        vaksinTimeTemp = vaccinePlaceData.vaksinTime;
        seccondVaksinTimeTemp = seccondVaksinTime;

        if (vaccinePlaceData.level >= vaccineLevelSystem.Length) return;
        upgradePrice = vaccineLevelSystem[vaccinePlaceData.level].price;
    }

    public bool CheckMaxLevel()
    {
        return vaccinePlaceData.level >= vaccineLevelSystem.Length;
    }
    public int Level
    {
        set { vaccinePlaceData.level = value; }
        get { return vaccinePlaceData.level; }
    }
    public int UpgradePrice
    {
        get { return upgradePrice; }
    }
    public int VaksinRate
    {
        get { return vaccinePlaceData.vaksinRate; }
    }
    public float VaksinTime
    {
        get { return vaccinePlaceData.vaksinTime; }
    }

    public VaccineLevelSystem GetNextValue(int x)
    {
        return vaccineLevelSystem[x];
    }
}
