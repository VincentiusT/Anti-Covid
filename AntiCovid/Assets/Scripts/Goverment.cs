using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Goverment : MonoBehaviour
{
    public static Goverment instance;
    [SerializeField] private DayManager dayManager;
    [SerializeField] private int money;
    [SerializeField] private float populationDivision = 10000f;
    private int moneyRate = 5; //get money per second
    [SerializeField] private PolicyData[] policyDatas;

    [SerializeField] private GameObject PSBBPanel;
    [SerializeField] private GameObject lockDownPanel;
    [SerializeField] private GameObject socializationPanel;
    [SerializeField] private GameObject moneyBoostPanel;
    [SerializeField] private GameObject buyVaksinPanel;

    [SerializeField] private TextMeshProUGUI moneyText;
    private TextMeshProUGUI PSBBpriceText;
    private TextMeshProUGUI lockDownPriceText;
    private TextMeshProUGUI socializationPriceText;
    private TextMeshProUGUI moneyBoostPriceText;
    private TextMeshProUGUI buyVaccineText;

    private Button PSBBButton;
    private Button lockDownButton;
    private Button socializationButton;
    private Button moneyBoostButton;

    public GameObject govermentPanel;
    private float timeToGetMoney = 5f;
    private float timeToGetMoneyTemp;

    private float PSBBdecreaseRate = 0.2f;
    private float lockDownDecreaseRate = 0.5f;
    private float socializationIncreaseRate = 0.1f;
    private float moneyBoostIncreaseRate = 0.1f;

    [SerializeField] private int PSBBprice = 100;
    [SerializeField] private int lockDownPrice = 300;
    [SerializeField] private int socializationPrice = 200;
    [SerializeField] private int vaccinePrice = 200;
    [SerializeField] private int moneyBoostPrice = 200;
    [SerializeField] private int vaccineStock = 50;

    private int PSBBDuration = 3;
    private int lockDownDuration = 3;
    private int socializationDuration = 5;
    private int moneyBoostDuration = 3;

    private float timeToDecreasePSBB = 15f, timeToDecreasePSBBTemp;
    private float timeToDecreaseLockDown = 15f, timeToDecreaseLockDownTemp;
    private float timeToIncreaseSocialization = 15f, timeToIncreaseSocializationTemp;

    private bool isPSBB, isLockDown, isSocialization, isMoneyBoost;
    private int PSBBStartDay, lockDownStartDay, socializationStartDay, moneyBoostStartDay;
    private int PSBBEndDay, lockDownEndDay, socializationEndDay, moneyBoostEndDay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PSBBpriceText = PSBBPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        lockDownPriceText = lockDownPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        socializationPriceText = socializationPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        moneyBoostPriceText = moneyBoostPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();
        buyVaccineText = buyVaksinPanel.transform.Find("price").GetComponent<TextMeshProUGUI>();

        PSBBButton = PSBBPanel.GetComponent<Button>();
        lockDownButton = lockDownPanel.GetComponent<Button>();
        socializationButton = socializationPanel.GetComponent<Button>(); 
        moneyBoostButton = moneyBoostPanel.GetComponent<Button>();

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
            moneyRate = Mathf.RoundToInt(Citizen.instance.HealthyPeoples / populationDivision);
            if (isMoneyBoost)
            {
                moneyRate = moneyRate + Mathf.RoundToInt(moneyRate * moneyBoostIncreaseRate);
            }
            money += moneyRate;
            timeToGetMoney = timeToGetMoneyTemp;
        }
        else
        {
            timeToGetMoney -= Time.deltaTime;
        }

        if(money < 10) moneyText.text = money.ToString("0");
        else moneyText.text = money.ToString("#,#");


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
                PSBBpriceText.text = "Price: " + PSBBprice.ToString("0");
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

                lockDownPriceText.text = "Price: " + lockDownPrice.ToString("0");
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

                socializationPriceText.text = "Price: " + socializationPrice.ToString("0");
                Debug.Log("policyEnd");
            }
            else
            {
                if (timeToIncreaseSocialization <= 0)
                {
                    float rate = Citizen.instance.Awareness * socializationIncreaseRate / socializationDuration;
                    if (Citizen.instance.Awareness + rate == 100) Citizen.instance.Awareness = 100;
                    else Citizen.instance.Awareness += rate;
                    timeToIncreaseSocialization = timeToIncreaseSocializationTemp;
                }
                else
                {
                    timeToIncreaseSocialization -= Time.deltaTime;
                }
                socializationPriceText.text = "Running : " + (socializationEndDay - dayManager.getDay()).ToString("0") + "days left";
            }
        }
        if (isMoneyBoost)
        {
            if (dayManager.getDay() >= moneyBoostEndDay)
            {
                isMoneyBoost = false;
                moneyBoostButton.interactable = true;

                moneyBoostPriceText.text = "Price: " + moneyBoostPrice.ToString("0");
                Debug.Log("policyEnd");
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

    public void MoneyBoost()
    {
        if (money < moneyBoostPrice) return;
        else money -= moneyBoostPrice;

        isMoneyBoost = true;
        moneyBoostStartDay = dayManager.getDay();
        moneyBoostEndDay = socializationStartDay + moneyBoostDuration;
        moneyBoostButton.interactable = false;
    }


    public void BuyVaccineStock()
    {
        if (money < vaccinePrice) return;
        else money -= vaccinePrice;

        VaksinManager.instance.VaccineStock += vaccineStock;
    }
    public int Money
    {
        set {
            //if (isMoneyBoost)
            //{
            //    int moneyIncrease = value - money;
            //    if (moneyIncrease > 0)
            //    {
            //        moneyIncrease = moneyIncrease + Mathf.RoundToInt(moneyIncrease * moneyBoostIncreaseRate);
            //    }
            //    money += moneyIncrease;
            //}
            //else
                money = value;
        }
        get { return money; }
    }
}
