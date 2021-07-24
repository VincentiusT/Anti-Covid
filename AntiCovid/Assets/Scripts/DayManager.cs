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
    private bool isChangingLight = false;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > secondsPerDay) //ganti hari
        {
            day++;
            UpdateDayUI();
            timeElapsed = 0f;
            StartCoroutine(ChangeLightGradually(-2f));
        }
        if(timeElapsed > secondsPerDay / 2f && !isChangingLight) //ganti jadi malem
        {
            isChangingLight = true;
            StartCoroutine(ChangeLightGradually(2f));
        }
    }

    public void UpdateDayUI()
    {
        onDayChangeCallback(day);
    }

    public int getDay()
    {
        return day;
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
}
