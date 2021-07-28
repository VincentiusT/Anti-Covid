using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacy : MonoBehaviour
{
    private int level = 1;

    private int transmissionDecreaseRate = 5;

    private SpriteRenderer sprite;
   
    [SerializeField] private PharmacyLevelSystem[] pharmacyLevelSystem;
    private int upgradePrice;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        upgradePrice = pharmacyLevelSystem[1].prices[LevelConverter.ConvertLevelToIndex()];
        transmissionDecreaseRate = pharmacyLevelSystem[0].transmissionDecreaseRate;
        sprite.sprite = pharmacyLevelSystem[0].sprite;
    }

    void Start()
    {
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
        sprite.sprite = pharmacyLevelSystem[level -1].sprite;
        DecreaseTransmissionIncreaseRate();
        if (level >= pharmacyLevelSystem.Length) return;
        upgradePrice = pharmacyLevelSystem[level].prices[LevelConverter.ConvertLevelToIndex()];
    }

    private void DecreaseTransmissionIncreaseRate()
    {
        if(Citizen.instance.TransmissionIncreaseRate  - transmissionDecreaseRate <= 5)
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
    public Sprite GetSprite()
    {
        return sprite.sprite;
    }
}
