using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    public delegate void OnMultiplierChange(float multiplier);
    public static event OnMultiplierChange onMultiplierChange;

    
    [SerializeField] private float timeToKeepMultiplier = 1f;
    [SerializeField] private float maxMultiplier = 3f;
    private float timeElapsedSinceLastTap = 0f;

    [SerializeField] private float multiplier = 1.0f;
    [SerializeField] private SpeedUpManager speedUpManager;
    private float prevMultiplier = 1.0f;
    public int tapAmount = 0;

   

    public void TapHospitalize()
    {
        if (AudioManager.instance!=null) AudioManager.instance.Play("tap");
        if (HospitalManager.instance.placeCount() < 1) UIManager.instance.ShowNotifPanel("You don't have any hospital!\nTry to build a hospital first before hospitalize people.");

        timeElapsedSinceLastTap = 0f;
        tapAmount++;

        CalculateMultiplier();

        HospitalManager.instance.HospitalizePeople(multiplier, speedUpManager.GetTimeMultiplier());
    }

    private void CalculateMultiplier()
    {
        if(multiplier >= maxMultiplier)
        {
            multiplier = maxMultiplier;
            return;
        }
            
        int x = (int)(tapAmount / 10);
        multiplier = 1 + (x / 10f);

        CheckMultiplierChange(multiplier);
    }

    private void CheckMultiplierChange(float multiplier)
    {
        if(multiplier != prevMultiplier)
        {
            onMultiplierChange(multiplier);
            prevMultiplier = multiplier;
        }
    }

    private void Update()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                if (hit.collider.name == "hospitalizeButton")
                {
                    TapHospitalize();
                }
            }
            //if (Input.touches[i])
           // TapHospitalize();
        }

        timeElapsedSinceLastTap += Time.deltaTime;
        if(timeElapsedSinceLastTap > timeToKeepMultiplier)
        {
            ResetMultiplier();
        }
    }

    private void ResetMultiplier()
    {
        tapAmount = 0;
        multiplier = 1.0f;
        CheckMultiplierChange(multiplier);
    }


}
