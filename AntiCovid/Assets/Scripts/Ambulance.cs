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
    [SerializeField] private AmbulanceLevelSystem[] ambulanceLevelSystem;
    private Animator ambulanceAnimations;
    private int pickUpRate = 50; //peoples per pickup

    private float pickUpTime = 10;
    private float pickUpTimeTemp;

    private int upgradePrice;

    private void Awake()
    {
        upgradePrice = ambulanceLevelSystem[1].price;
        pickUpTime = ambulanceLevelSystem[0].pickupTime;
        pickUpRate = ambulanceLevelSystem[0].pickupRate;
        pickUpTimeTemp = pickUpTime;
    }

    private void Start()
    {
        ambulanceAnimations = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (pickUpTime <= 0)
        {
            Debug.Log("pickup time: " + pickUpTime);
            playAmbulanceAnimation(Random.Range(1, 4));
            //PickUpSickPeoples();
            pickUpTime = pickUpTimeTemp;
        }
        else
        {
            pickUpTime -= Time.deltaTime;
        }
    }

    private void playAmbulanceAnimation(int index)
    {
        ambulanceAnimations.SetInteger("play", index);
    }

    public void resetAnimation()
    {
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
        pickUpTimeTemp = pickUpTime;
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
