using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacy : MonoBehaviour
{
    public PharmacyData pharmacyData;

    //private int level = 1;

    //private int transmissionDecreaseRate = 5;

    private SpriteRenderer sprite;
   
    [SerializeField] private PharmacyLevelSystem[] pharmacyLevelSystem;
    private int upgradePrice;

    private void Awake()
    {
        pharmacyData.level = 1;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void AssignLevelSystem(PharmacyLevelSystem[] lvl)
    {
        pharmacyLevelSystem = lvl;
        upgradePrice = pharmacyLevelSystem[1].price;
        pharmacyData.transmissionDecreaseRate = pharmacyLevelSystem[0].transmissionDecreaseRate;
        sprite.sprite = pharmacyLevelSystem[0].sprite;
    }

    void Start()
    {
        DecreaseTransmissionIncreaseRate();
    }

    public bool CheckMaxLevel()
    {
        return pharmacyData.level >= pharmacyLevelSystem.Length;
    }

    public void UpgradePharmacy()
    {
        pharmacyData.level++;

        pharmacyData.transmissionDecreaseRate = pharmacyLevelSystem[pharmacyData.level - 1].transmissionDecreaseRate;
        sprite.sprite = pharmacyLevelSystem[pharmacyData.level -1].sprite;
        DecreaseTransmissionIncreaseRate();
        if (pharmacyData.level >= pharmacyLevelSystem.Length) return;
        upgradePrice = pharmacyLevelSystem[pharmacyData.level].price;
    }

    private void DecreaseTransmissionIncreaseRate()
    {
        if(Citizen.instance.TransmissionIncreaseRate  - pharmacyData.transmissionDecreaseRate <= 5)
        {
            Citizen.instance.TransmissionIncreaseRate = 5;
            return;
        }

        Citizen.instance.TransmissionIncreaseRate -= pharmacyData.transmissionDecreaseRate;
    }

    public int Level
    {
        set { pharmacyData.level = value; }
        get { return pharmacyData.level; }
    }
    public int UpgradePrice
    {
        get { return upgradePrice; }
    }

    public int TransmissionDecreaseRate
    {
        get { return pharmacyData.transmissionDecreaseRate; }
    }
    public Sprite GetSprite()
    {
        return sprite.sprite;
    }

    public PharmacyLevelSystem GetNextValue(int x)
    {
        return pharmacyLevelSystem[x];
    }
}
