using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Goverment : MonoBehaviour
{
    public static Goverment instance;
    [SerializeField] private DayManager dayManager;
    private int money;
    private int moneyRate = 10; //get money per second
    [SerializeField] private PolicyData[] policyDatas;

    [SerializeField] private GameObject PSBBPanel;
    [SerializeField] private GameObject lockDownPanel;
    [SerializeField] private GameObject socializationPanel;

    [SerializeField] private TextMeshProUGUI moneyText;
    private TextMeshProUGUI PSBBpriceText;
    private TextMeshProUGUI lockDownPriceText;
    private TextMeshProUGUI socializationPriceText;

    private Button PSBBButton;
    private Button lockDownButton;
    private Button socializationButton;

    public GameObject govermentPanel;
    private float timeToGetMoney = 1f;
    private float timeToGetMoneyTemp;

    private float PSBBdecreaseRate = 0.2f;
    private float lockDownDecreaseRate = 0.5f;
    private float socializationIncreaseRate = 0.3f;

    private int PSBBprice = 100;
    private int lockDownPrice = 300;
    private int socializationPrice = 200;

    private int PSBBDuration = 3;
    private int lockDownDuration = 3;
    private int socializationDuration = 5;

    private float timeToDecreasePSBB = 15f, timeToDecreasePSBBTemp;
    private float timeToDecreaseLockDown = 15f, timeToDecreaseLockDownTemp;
    private float timeToIncreaseSocialization = 15f, timeToIncreaseSocializationTemp;

    private bool isPSBB, isLockDown, isSocialization;
    private int PSBBStartDay, lockDownStartDay, socializationStartDay;
    private int PSBBEndDay, lockDownEndDay, socializationEndDay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PSBBpriceText = PSBBPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        lockDownPriceText = lockDownPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        socializationPriceText = socializationPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();

        PSBBButton = PSBBPanel.GetComponent<Button>();
        lockDownButton = lockDownPanel.GetComponent<Button>();
        socializationButton = socializationPanel.GetComponent<Button>(); 

        timeToGetMoneyTemp = timeToGetMoney;
        PSBBpriceText.text = "Price: " + PSBBprice.ToString("0");
        lockDownPriceText.text = "Price: " + lockDownPrice.ToString("0");
        socializationPriceText.text = "Price: " + socializationPrice.ToString("0");
        timeToDecreaseLockDownTemp = timeToDecreaseLockDown;
        timeToDecreasePSBBTemp = timeToDecreasePSBB;
        timeToIncreaseSocializationTemp = timeToIncreaseSocialization;
    }

    void Update()
    {
        if (timeToGetMoney <= 0)
        {
            money += moneyRate;
            timeToGetMoney = timeToGetMoneyTemp;
        }
        else
        {
            timeToGetMoney -= Time.deltaTime;
        }

        moneyText.text = money.ToString("0");


        RunPolicies();
    }
    public void ShowGovermentPanel(bool show)
    {
        govermentPanel.SetActive(show);
    }

    private void RunPolicies()
    {
        if (isPSBB)
        {
            if (dayManager.getDay() >= PSBBEndDay)
            {
                isPSBB = false;
                PSBBButton.interactable = true;
                Debug.Log("policyEnd");
            }
            else
            {
                if(timeToDecreasePSBB <= 0)
                {
                    float rate = (float)Citizen.instance.TransmissionRateTotal * PSBBdecreaseRate / PSBBDuration;
                    Citizen.instance.TransmissionRateTotal -= (int)rate;
                    timeToDecreasePSBB = timeToDecreasePSBBTemp;
                }
                else
                {
                    timeToDecreasePSBB -= Time.deltaTime;
                }
                PSBBpriceText.text = "Running : " +  (PSBBEndDay -  dayManager.getDay()).ToString("0") + "days left";
            }
        }
        if (isLockDown)
        {
            if (dayManager.getDay() >= lockDownEndDay)
            {
                isLockDown = false;
                lockDownButton.interactable = true;
                Debug.Log("policyEnd");
            }
            else
            {
                if (timeToDecreaseLockDown <= 0)
                {
                    float rate = (float)Citizen.instance.TransmissionRateTotal * lockDownDecreaseRate / lockDownDuration;
                    Citizen.instance.TransmissionRateTotal -= (int)rate;
                    timeToDecreaseLockDown = timeToDecreaseLockDownTemp;
                }
                else
                {
                    timeToDecreaseLockDown -= Time.deltaTime;
                }
                lockDownPriceText.text = "Running : " + (lockDownEndDay - dayManager.getDay()).ToString("0") + "days left";
            }
        }
        if (isSocialization)
        {
            if (dayManager.getDay() >= socializationEndDay)
            {
                isSocialization = false;
                socializationButton.interactable = true;
                Debug.Log("policyEnd");
            }
            else
            {
                if (timeToIncreaseSocialization <= 0)
                {
                    float rate = Citizen.instance.Awareness * socializationIncreaseRate / socializationDuration;
                    Citizen.instance.Awareness += rate;
                    timeToIncreaseSocialization = timeToIncreaseSocializationTemp;
                }
                else
                {
                    timeToIncreaseSocialization -= Time.deltaTime;
                }
                socializationPriceText.text = "Running : " + (socializationEndDay - dayManager.getDay()).ToString("0") + "days left";
            }
        }
    }

    public void PSBB()
    {
        if (money < PSBBprice)return;
        
        else money -= PSBBprice;

        isPSBB = true;
        PSBBStartDay = dayManager.getDay();
        PSBBEndDay = PSBBStartDay + PSBBDuration;
        PSBBButton.interactable = false;
        
    }

    public void LockDown()
    {
        if (money < lockDownPrice)return;
        else  money -= lockDownPrice;
        
        isLockDown = true;
        lockDownStartDay = dayManager.getDay();
        lockDownEndDay = lockDownStartDay + lockDownDuration;
        lockDownButton.interactable = false;
    }

    public void Socialization()
    {
        if (money < socializationPrice)return;
        else money -= socializationPrice;
        
        isSocialization = true;
        socializationStartDay = dayManager.getDay();
        socializationEndDay = socializationStartDay + socializationDuration;
        socializationButton.interactable = false;
    }

    public int Money
    {
        set { money = value; }
        get { return money; }
    }
}
