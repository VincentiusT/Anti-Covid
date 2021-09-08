using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public delegate void OnDayChange(int _day);
    public static event OnDayChange onDayChangeCallback;

    private float timeElapsed = 0f;
    private int day = 0;
    [SerializeField] private int secondsPerDay = 5;
    [SerializeField] private GameObject overlayLight;
    [Range(0, 99)][SerializeField] private int rainChance = 20;
    [SerializeField] private ParticleSystem rainParticleSystem;
    private bool isChangingLight = false;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > secondsPerDay) //ganti hari
        {
            ChangeDay();
        }
        if(timeElapsed > secondsPerDay / 2f && !isChangingLight) //ganti jadi malem
        {
            isChangingLight = true;
            StartCoroutine(ChangeLightGradually(2f));
        }
    }

    private void ChangeDay()
    {
        day++;
        DetermineRainDay();
        UpdateDayUI();
        timeElapsed = 0f;
        StartCoroutine(ChangeLightGradually(-2f));
    }

    public void UpdateDayUI()
    {
        onDayChangeCallback(day);
    }

    public int getDay()
    {
        return day;
    }
    public void setDay(int _day)
    {
        day = _day;
    }

    private IEnumerator ChangeLightGradually(float speed)
    {
        while (true)
        {
            Color color = overlayLight.GetComponent<SpriteRenderer>().color;
            if ((speed > 0 && color.a >= 0.58f) || (speed < 0 && color.a <= 0f))
                break;
            
            color.a += speed * Time.deltaTime;
            overlayLight.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
        isChangingLight = false;
    }

    private void DetermineRainDay()
    {
        int randomRainChance = UnityEngine.Random.Range(0, 100);
        if(randomRainChance <= rainChance)
        {
            Debug.Log("HUJAN WOI");
            int rainDelay = UnityEngine.Random.Range(0, secondsPerDay - Mathf.RoundToInt(secondsPerDay * 0.8f));
            int rainDuration = UnityEngine.Random.Range(0, secondsPerDay - rainDelay);
            int rainType = UnityEngine.Random.Range(0, 3);

            StopAllCoroutines();
            if(rainParticleSystem.isPlaying) rainParticleSystem.Stop();
            StartCoroutine(ExcecuteRain(rainDelay, rainDuration, rainType));
        }
    }

    private IEnumerator ExcecuteRain(int secondDelay, int rainDuration, int rainType)
    {
        yield return new WaitForSeconds(secondDelay);
        SetParticleCountToRainType(rainType);
        rainParticleSystem.Play();
        yield return new WaitForSeconds(rainDuration);
        rainParticleSystem.Stop();
    }

    private void SetParticleCountToRainType(int rainType)
    {
        var emission = rainParticleSystem.emission;

        if(rainType == 0) //gerimis
        {
            emission.rateOverTime = 25;
        }
        else if(rainType == 1) //medium
        {
            emission.rateOverTime = 100;
        }
        else //lebat
        {
            emission.rateOverTime = 500;
        }

    }
}
