using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject canvas;
    private TextMeshProUGUI vaccineStockText;

    private TextMeshProUGUI textDay;
    private GameObject DayPopUpPanel;

    private TextMeshProUGUI multiplierText;
    private GameObject multiplierPanel;

    private GameObject notifPanel;
    private TextMeshProUGUI notifText;

    private GameObject rewardClaimPanel;
    private Button hospitalizedButton;
    bool done;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        vaccineStockText = canvas.transform.Find("SafeArea/GamePanel/vaccineCountPanel/vaccineCount").GetComponent<TextMeshProUGUI>();

        textDay = canvas.transform.Find("SafeArea/GamePanel/DayPanel/DayCount").GetComponent<TextMeshProUGUI>();
        DayPopUpPanel = canvas.transform.Find("SafeArea/GamePanel/DayInformationPanel").gameObject;

        multiplierPanel = canvas.transform.Find("SafeArea/GamePanel/MultiplierPanel").gameObject;
        multiplierText = canvas.transform.Find("SafeArea/GamePanel/MultiplierPanel/multiplier").GetComponent<TextMeshProUGUI>();

        notifPanel = canvas.transform.Find("SafeArea/GamePanel/notifPanel").gameObject;
        notifText = notifPanel.transform.Find("info/info").GetComponent<TextMeshProUGUI>();

        rewardClaimPanel = canvas.transform.Find("ConfirmRewardPanel").gameObject;
        hospitalizedButton = canvas.transform.Find("SafeArea/GamePanel/hospitalizeButton").GetComponent<Button>();

        SubscribeToDayEvent();
        SubscribeToMultiplierEvent();
    }

    private void FixedUpdate()
    {
        if (Citizen.instance.SickPeoples < 1)
        {
            hospitalizedButton.interactable = false;
            done = false;
        }
        else
        {
            if (!done)
            {
                hospitalizedButton.interactable = true;
                done = true;
            }
        }
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
        if(multiplierPanel != null)
            multiplierPanel.GetComponent<Animator>().SetBool("Show", show);
    }



    #endregion

    public void UpgradeHospitalButton()
    {
        HospitalManager.instance.UpgradeAllAttribute();
    }

    public void UpgradeAmbulanceButton()
    {
        AmbulanceManager.instance.UpgradeAllAttribute();
    }

    public void UpgradeVaccinePlaceButton()
    {
        VaksinManager.instance.UpgradeAllAttribute();
    }

    public void UpgradeOfficerPlaceButton()
    {
        OfficerManager.instance.UpgradeAllAttribute();
    }

    public void UpgradePharmacyButton()
    {
        PharmacyManager.instance.UpgradeAllAttribute();
    }

    public void UpdateVaccineStockUI(int stock)
    {
        if (stock < 10)
        {
            vaccineStockText.text = stock.ToString("0");
        }
        else
        {
            vaccineStockText.text = stock.ToString("#,#");
        }
    }
    public void ShowNotifPanel(string text)
    {
        notifText.text = text;
        notifPanel.GetComponent<Animator>().SetTrigger("show");
        if (AudioManager.instance != null) AudioManager.instance.Play("notif");
    }

    public void OpenRandomEventRewardClaimPanel(RandomEventType randomEventType)
    {
        rewardClaimPanel.SetActive(true);
        Time.timeScale = 0f;
        RewardParticleManager.Instance.PlayRewardParticle(randomEventType);
    }

    public void CloseRewardClaimPanel()
    {
        rewardClaimPanel.SetActive(false);
        ResumeGame();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
}
