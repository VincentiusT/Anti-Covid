using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private TextMeshProUGUI textDay;
    private GameObject DayPopUpPanel;
    public static UIManager instance;

    private TextMeshProUGUI multiplierText;
    private GameObject multiplierPanel;

    private GameObject notifPanel;
    private TextMeshProUGUI notifText;

    private void Start()
    {
        if(instance == null)
            instance = this;

        textDay = canvas.transform.Find("GamePanel/DayPanel/DayCount").GetComponent<TextMeshProUGUI>();
        DayPopUpPanel = canvas.transform.Find("GamePanel/DayInformationPanel").gameObject;

        multiplierPanel = canvas.transform.Find("GamePanel/MultiplierPanel").gameObject;
        multiplierText = canvas.transform.Find("GamePanel/MultiplierPanel/multiplier").GetComponent<TextMeshProUGUI>();

        notifPanel = canvas.transform.Find("GamePanel/notifPanel").gameObject;
        notifText = notifPanel.transform.Find("info/info").GetComponent<TextMeshProUGUI>();

        SubscribeToDayEvent();
        SubscribeToMultiplierEvent();
    }

    

    #region DayRegion

    private void SubscribeToDayEvent()
    {
        DayManager.onDayChangeCallback += ChangeDayText;
        DayManager.onDayChangeCallback += ShowDayPopUpPanel;
    }

    void ChangeDayText(int day)
    {
        textDay.text = day.ToString();
    }

    void ShowDayPopUpPanel(int day)
    {
        DayPopUpPanel.SetActive(true);
        DayPopUpPanel.GetComponent<Animator>().Play("DayPopUp");
        DayPopUpPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Day " + day.ToString();
        StartCoroutine(CountdownCloseDayPopUp());
    }

    IEnumerator CountdownCloseDayPopUp()
    {
        yield return new WaitForSeconds(2f);
        DayPopUpPanel.SetActive(false);
    }

    #endregion

    #region TapRegion

    private void SubscribeToMultiplierEvent()
    {
        TapManager.onMultiplierChange += ChangeMultiplierText;
    }

    private void ChangeMultiplierText(float multiplier)
    {
        multiplierText.text = multiplier.ToString(".0") + "x";

        if(multiplier == 1f)
            ShowMultiplierPanel(false);
        else
            ShowMultiplierPanel(true);

    }

    private void ShowMultiplierPanel(bool show)
    {
        multiplierPanel.GetComponent<Animator>().SetBool("Show", show);
    }



    #endregion

    public void ShowNotifPanel(string text)
    {
        notifText.text = text;
        notifPanel.GetComponent<Animator>().SetTrigger("show");
    }
}
