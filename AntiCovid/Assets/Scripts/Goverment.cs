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
    public GameObject govermentPanel;
    private float timeToGetMoney = 1f;
    private float timeToGetMoneyTemp;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeToGetMoneyTemp = timeToGetMoney;
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
        float rate = (float)Citizen.instance.TransmissionRate * 0.5f;
        Citizen.instance.TransmissionRate -= (int)rate;
    }

    public void LockDown()
    {
        float rate = (float)Citizen.instance.TransmissionRate * 0.9f;
        Citizen.instance.TransmissionRate -= (int)rate;
    }

    public int Money
    {
        set { money = value; }
        get { return money; }
    }
}
