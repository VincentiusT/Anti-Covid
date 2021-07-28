using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{
    private int level = 1;
    /*  lvl 1 = 10 
     *  lvl 2 = 30
     *  lvl 3 = 60
     * 
     */
    public AmbulanceLevelSystem[] ambulanceLevelSystem;
    private Animator ambulanceAnimations;
    private int pickUpRate = 50; //peoples per pickup

    private float pickUpTime = 10;
    private float pickUpTimeMax = 20;

    private float realPickUpTime;

    private int upgradePrice;

    private void Awake()
    {
        
    }
    public void AssignLevelSystem(AmbulanceLevelSystem[] lvl)
    {
        ambulanceLevelSystem = lvl;
        upgradePrice = ambulanceLevelSystem[1].price;
        Debug.Log("hahahh : " + upgradePrice);
        pickUpTime = ambulanceLevelSystem[0].pickupTime;
        pickUpTimeMax = ambulanceLevelSystem[0].pickupTimeMax;
        pickUpRate = ambulanceLevelSystem[0].pickupRate;
        realPickUpTime = Random.Range(pickUpTime, pickUpTimeMax);
        ambulanceAnimations = GetComponent<Animator>();
    }

    private void Start()
    {
        

    }

    private void Update()
    {
        if (realPickUpTime <= 0)
        {
            Debug.Log("pickup time: " + realPickUpTime);
            playAmbulanceAnimation(Random.Range(1, 4));
            //PickUpSickPeoples();
            realPickUpTime = Random.Range(pickUpTime, pickUpTimeMax);
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
        if (AudioManager.instance != null) AudioManager.instance.Stop("ambulance");
        ambulanceAnimations.SetInteger("play", 0);
    }

    public void PickUpSickPeoples()
    {
        Debug.Log("dari ambulan " + pickUpRate +" orang");
        HospitalManager.instance.HospitalizePeopleFromAmbulance(pickUpRate);
    }

    public bool CheckMaxLevel()
    {
        return level >= ambulanceLevelSystem.Length;
    }

    public void UpgradePharmacy()
    {
        level++;

        pickUpRate = ambulanceLevelSystem[level - 1].pickupRate;
        pickUpTime = ambulanceLevelSystem[level - 1].pickupTime;
        pickUpTimeMax = ambulanceLevelSystem[level - 1].pickupTimeMax;
        realPickUpTime = Random.Range(pickUpTime, pickUpTimeMax);
        if (level >= ambulanceLevelSystem.Length) return;
        upgradePrice = ambulanceLevelSystem[level].price;
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

    public int PickupRate
    {
        get { return pickUpRate; }
    }

    public float PickupTime
    {
        get { return pickUpTime; }
    }
}
