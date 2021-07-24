using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goverment : MonoBehaviour
{
    public static Goverment instance;

    private int money;
    private int moneyRate = 10; //get money per second

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI PSBBpriceText;
    [SerializeField] private TextMeshProUGUI lockDownPriceText;

    public GameObject govermentPanel;
    private float timeToGetMoney = 1f;
    private float timeToGetMoneyTemp;

    private float PSBBdecreaseRate = 0.5f;
    private float lockDownDecreaseRate = 0.9f;
    private float socializationIncreaseRate = 0.3f;

    private int PSBBprice = 100;
    private int lockDownPrice = 300;
    private int socializationPrice = 200;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeToGetMoneyTemp = timeToGetMoney;
        PSBBpriceText.text = "Price: " + PSBBprice.ToString("0");
        lockDownPriceText.text = "Price: " + lockDownPrice.ToString("0");
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
    }

    public void ShowGovermentPanel(bool show)
    {
        govermentPanel.SetActive(show);
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
        float rate = (float)Citizen.instance.TransmissionRateTotal * PSBBdecreaseRate;
        Citizen.instance.TransmissionRateTotal -= (int)rate;
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
        float rate = (float)Citizen.instance.TransmissionRateTotal * lockDownDecreaseRate;
        Citizen.instance.TransmissionRateTotal -= (int)rate;
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
        float rate = Citizen.instance.Awareness * socializationIncreaseRate;
        Citizen.instance.Awareness += rate;
    }

    public int Money
    {
        set { money = value; }
        get { return money; }
    }
}
