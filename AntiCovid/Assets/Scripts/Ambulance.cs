using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{
    public AmbulanceData ambulanceData;
    //private int level = 1;
    /*  lvl 1 = 10 
     *  lvl 2 = 30
     *  lvl 3 = 60
     * 
     */
    public AmbulanceLevelSystem[] ambulanceLevelSystem;
    private Animator ambulanceAnimations;
    //private int pickUpRate = 50; //peoples per pickup

    //private float pickUpTime = 10;
    //private float pickUpTimeMax = 20;

    private float realPickUpTime;

    private int upgradePrice;

    private void Awake()
    {
        ambulanceData.level = 1;
    }
    public void AssignLevelSystem(AmbulanceLevelSystem[] lvl)
    {
        ambulanceLevelSystem = lvl;
        upgradePrice = ambulanceLevelSystem[1].price;
        Debug.Log("hahahh : " + upgradePrice);
        ambulanceData.pickUpTime = ambulanceLevelSystem[0].pickupTime;
        ambulanceData.pickUpTimeMax = ambulanceLevelSystem[0].pickupTimeMax;
        ambulanceData.pickUpRate = ambulanceLevelSystem[0].pickupRate;
        realPickUpTime = Random.Range(ambulanceData.pickUpTime, ambulanceData.pickUpTimeMax);
        ambulanceAnimations = GetComponent<Animator>();
    }

    private void Start()
    {
        

    }

    private void Update()
    {
        if (realPickUpTime <= 0)
        {
            playAmbulanceAnimation(Random.Range(1, 4));
            //PickUpSickPeoples();
            realPickUpTime = Random.Range(ambulanceData.pickUpTime, ambulanceData.pickUpTimeMax);
            Debug.Log("pickup time: " + realPickUpTime);
        }
        else
        {
            realPickUpTime -= Time.deltaTime;
        }
    }

    private void playAmbulanceAnimation(int index)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("ambulance");
        ambulanceAnimations.SetInteger("play", index);
    }

    public void resetAnimation()
    {
        Debug.Log("why ga stop");
        if (AudioManager.instance != null) AudioManager.instance.Stop("ambulance");
        ambulanceAnimations.SetInteger("play", 0);
    }

    public void PickUpSickPeoples()
    {
        Debug.Log("dari ambulan " + ambulanceData.pickUpRate +" orang");
        HospitalManager.instance.HospitalizePeopleFromAmbulance(ambulanceData.pickUpRate);
    }

    public bool CheckMaxLevel()
    {
        return ambulanceData.level >= ambulanceLevelSystem.Length;
    }

    public void UpgradePharmacy()
    {
        ambulanceData.level++;

        ambulanceData.pickUpRate = ambulanceLevelSystem[ambulanceData.level - 1].pickupRate;
        ambulanceData.pickUpTime = ambulanceLevelSystem[ambulanceData.level - 1].pickupTime;
        ambulanceData.pickUpTimeMax = ambulanceLevelSystem[ambulanceData.level - 1].pickupTimeMax;
        realPickUpTime = Random.Range(ambulanceData.pickUpTime, ambulanceData.pickUpTimeMax);
        if (ambulanceData.level >= ambulanceLevelSystem.Length) return;
        upgradePrice = ambulanceLevelSystem[ambulanceData.level].price;
    }

    public int Level
    {
        set { ambulanceData.level = value; }
        get { return ambulanceData.level; }
    }
    public int UpgradePrice
    {
        get { return upgradePrice; }
    }

    public int PickupRate
    {
        get { return ambulanceData.pickUpRate; }
    }

    public float PickupTime
    {
        get { return ambulanceData.pickUpTime; }
    }

    public AmbulanceLevelSystem GetNextValue(int x)
    {
        return ambulanceLevelSystem[x];
    }
}
