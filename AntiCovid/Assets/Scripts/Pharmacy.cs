using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacy : MonoBehaviour
{
    private int level = 1;

    private int transmissionDecreaseRate = 5;
   
    [SerializeField] private PharmacyLevelSystem[] pharmacyLevelSystem;
    private int upgradePrice;

    private void Awake()
    {
        upgradePrice = pharmacyLevelSystem[1].price;
    }

    void Start()
    {
        transmissionDecreaseRate = pharmacyLevelSystem[0].transmissionDecreaseRate;

        DecreaseTransmissionIncreaseRate();
    }

    public bool CheckMaxLevel()
    {
        return level >= pharmacyLevelSystem.Length;
    }

    public void UpgradePharmacy()
    {
        level++;

        transmissionDecreaseRate = pharmacyLevelSystem[level - 1].transmissionDecreaseRate;
        DecreaseTransmissionIncreaseRate();
        if (level >= pharmacyLevelSystem.Length) return;
        upgradePrice = pharmacyLevelSystem[level].price;
    }

    private void DecreaseTransmissionIncreaseRate()
    {
        if(Citizen.instance.TransmissionIncreaseRate <= 5)
        {
            Citizen.instance.TransmissionIncreaseRate = 5;
            return;
        }

        Citizen.instance.TransmissionIncreaseRate -= transmissionDecreaseRate;
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

    public int TransmissionDecreaseRate
    {
        get { return transmissionDecreaseRate; }
    }
}
