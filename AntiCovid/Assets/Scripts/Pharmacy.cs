using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacy : MonoBehaviour
{
    private int level = 1;

    private int transmissionDecreaseRate = 5;
   
    /*  lvl 1 = 5 -> masker
     *  lvl 2 = 15 -> hand sanitizer
     *  lvl 3 = 50 -> obat obatan
     * 
     */
    [SerializeField] private PharmacyLevelSystem[] pharmacyLevelSystem;
    private int upgradePrice;

    private void Awake()
    {
        upgradePrice = pharmacyLevelSystem[1].price;
    }

    void Start()
    {
        transmissionDecreaseRate = pharmacyLevelSystem[0].transmissionDecreaseRate;

        Citizen.instance.TransmissionIncreaseRate -= transmissionDecreaseRate;
    }

    public bool CheckMaxLevel()
    {
        return level >= pharmacyLevelSystem.Length;
    }

    public void UpgradePharmacy()
    {
        level++;

        transmissionDecreaseRate = pharmacyLevelSystem[level - 1].transmissionDecreaseRate;
        Citizen.instance.TransmissionIncreaseRate -= transmissionDecreaseRate;
        if (level >= pharmacyLevelSystem.Length) return;
        upgradePrice = pharmacyLevelSystem[level].price;
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
