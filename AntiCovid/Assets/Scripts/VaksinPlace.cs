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

    private int upgradePrice;

    private void Awake()
    {
        upgradePrice = vaccineLevelSystem[1].price;
        vaksinRate = vaccineLevelSystem[0].vaksinRate;
        vaksinTime = vaccineLevelSystem[0].vaksinTime;
    }
    void Start()
    {
        vaksinTimeTemp = vaksinTime;   
    }

    void Update()
    {
        if(vaksinTime <= 0)
        {
            VaksinPeople(vaksinRate);
            vaksinTime = vaksinTimeTemp;
        }
        else
        {
            vaksinTime -= Time.deltaTime;
        }
    }

    public void VaksinPeople(int people)
    {
        Citizen.instance.VaksinedPeoples += people;
    }

    public void UpgradeVaksinPlace()
    {
        level++;

        vaksinRate = vaccineLevelSystem[level - 1].vaksinRate;
        vaksinTime = vaccineLevelSystem[level - 1].vaksinTime;
        vaksinTimeTemp = vaksinTime;

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
