using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goverment : MonoBehaviour
{
    public static Goverment instance;
    [SerializeField] private DayManager dayManager;
    private int money;
    private int moneyRate = 10; //get money per second
    [SerializeField] private PolicyData[] policyDatas;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI PSBBpriceText;
    [SerializeField] private TextMeshProUGUI lockDownPriceText;

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
        timeToGetMoneyTemp = timeToGetMoney;
        PSBBpriceText.text = "Price: " + PSBBprice.ToString("0");
        lockDownPriceText.text = "Price: " + lockDownPrice.ToString("0");
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
            }
        }
        if (isLockDown)
        {
            if (dayManager.getDay() >= lockDownEndDay)
            {
                isLockDown = false;
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
            }
        }
        if (isSocialization)
        {
            if (dayManager.getDay() >= socializationEndDay)
            {
                isSocialization = false;
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
                
            }
        }
    }

    public void PSBB()
    {
        if (money < PSBBprice)
        {
            return;
        }
        else
        {
            money -= PSBBprice;
        }
        
        isPSBB = true;
        PSBBStartDay = dayManager.getDay();
        PSBBEndDay = PSBBStartDay + PSBBDuration;
    }

    public void LockDown()
    {
        if (money < lockDownPrice)
        {
            return;
        }
        else
        {
            money -= lockDownPrice;
        }
        
        isLockDown = true;
        lockDownStartDay = dayManager.getDay();
        lockDownEndDay = lockDownStartDay + lockDownDuration;
    }

    public void Socialization()
    {
        if (money < socializationPrice)
        {
            return;
        }
        else
        {
            money -= socializationPrice;
        }
        
        isSocialization = true;
        socializationStartDay = dayManager.getDay();
        socializationEndDay = socializationStartDay + socializationDuration;
    }

    public int Money
    {
        set { money = value; }
        get { return money; }
    }
}
