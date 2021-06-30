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
    private int pickUpRate = 50; //peoples per pickup

    private float pickUpTime = 10;
    private float pickUpTimeTemp;

    private void Start()
    {
        pickUpTimeTemp = pickUpTime;
    }

    private void Update()
    {
        if (pickUpTime <= 0)
        {
            PickUpSickPeoples();
            pickUpTime = pickUpTimeTemp;
        }
        else
        {
            pickUpTime -= Time.deltaTime;
        }
    }

    public void PickUpSickPeoples()
    {
        Debug.Log("dari ambulan " + pickUpRate +" orang");
        HospitalManager.instance.HospitalizePeopleFromAmbulance(pickUpRate);
    }
}
