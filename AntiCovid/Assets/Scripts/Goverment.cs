using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goverment : MonoBehaviour
{
    private int money;
    private int moneyRate = 10; //get money per second

    [SerializeField]private TextMeshProUGUI moneyText;

    private float timeToGetMoney = 1f;
    private float timeToGetMoneyTemp;
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
}
