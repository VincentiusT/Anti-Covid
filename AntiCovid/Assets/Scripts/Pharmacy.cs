using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacy : MonoBehaviour
{
    private int level = 1;

    private int TransmissionDecreaseRate = 5;
    /*  lvl 1 = 5 -> masker
     *  lvl 2 = 15 -> hand sanitizer
     *  lvl 3 = 50 -> obat obatan
     * 
     */
 
    private int upgradePrice;

    void Start()
    {
        TransmissionDecreaseRate = getDecreaseRatByLevel();

        Citizen.instance.TransmissionRate -= TransmissionDecreaseRate;
    }

    int getDecreaseRatByLevel()
    {
        int n = 0;
        if (level == 1) n = 5;
        if (level == 2) n = 15;
        if (level == 3) n = 50;

        return n;
    }
}
