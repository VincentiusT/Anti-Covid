using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaksinPlace : MonoBehaviour
{
    private int level = 1;

    private int vaksinRate = 5; //people per vaksinTime
    /*  lvl 1 = 5 
     *  lvl 2 = 15 
     *  lvl 3 = 50 
     * 
     */

    private float vaksinTime = 10;
    private float vaksinTimeTemp;

    private int upgradePrice;

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
        Citizen.instance.VaksinedPeoples = people;
    }
}
