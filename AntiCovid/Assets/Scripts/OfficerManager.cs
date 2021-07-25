using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OfficerManager : MonoBehaviour
{
    public static OfficerManager instance;

    public GameObject officerPoint;
    public GameObject officerObj;
    public GameObject officerBuyPanel;

    public GameObject buyButton;

    private TextMeshProUGUI officerBuyText;
    private TextMeshProUGUI officerLevelText;
    private TextMeshProUGUI refillTimeText;
    private TextMeshProUGUI officerPriceText;

    [SerializeField] private int price = 10;
    private Officer officer;
    private bool alreadyBought;

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        officerBuyText = buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        officerLevelText = buyButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        refillTimeText = buyButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        officerPriceText = buyButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        officerPriceText.text = "Price: " + price;
    }

    public void ShowBuyOfficerPanel(bool show) //munculin buy panel
    {
        officerBuyPanel.SetActive(show);
    }

    public void BuyOfficer()
    {
        if (alreadyBought)
        {
            UpgradeOfficer();
            return;
        }
        if (officerPoint.transform.childCount >= 1)//klo udh beli lgsg return
        {
            return;
        }
        if (Goverment.instance.Money < price)
        {
            return;
        }
        else
        {
            Goverment.instance.Money -= price;
        }
        GameObject go = Instantiate(officerObj, officerPoint.transform.position, officerPoint.transform.rotation) as GameObject;
        go.transform.parent = officerPoint.transform;
        go.name = "Officer";

        officer = go.GetComponent<Officer>();

        ShowBuyOfficerPanel(false);
        alreadyBought = true;
        UpdateBuyUI();
    }

    private void UpdateBuyUI()
    {
        if (officer.CheckMaxLevel())
        {
            officerLevelText.text = "level: MAX";
            buyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            officerLevelText.text = "level: " + officer.Level;
            officerBuyText.text = "Upgrade";
        }
        refillTimeText.text = "Recharge Time: " + officer.RefillTime;
        officerPriceText.text = "Price: " + officer.UpgradePrice;
    }

    public void UpgradeOfficer()
    {
        if (officer.CheckMaxLevel()) return;

        Debug.Log("upgrade price: " + officer.UpgradePrice);
        if (Goverment.instance.Money >= officer.UpgradePrice)
        {
            Goverment.instance.Money -= officer.UpgradePrice;
        }
        else
        {
            return;
        }

        officer.UpgradeOfficer();
        UpdateBuyUI();
    }
}
