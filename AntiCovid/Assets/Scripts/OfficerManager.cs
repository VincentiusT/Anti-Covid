using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OfficerManager : MonoBehaviour
{
    public static OfficerManager instance;

    public GameObject upgradePanel;
    private GameObject upgradeButton;
    private TextMeshProUGUI upgradeLevelText, upgradeRefillRateText, upgradePriceText;
    private Image upgradeSprite;
    private int currentSelected;

    public GameObject officerPoint;
    public GameObject officerObj;
    public GameObject officerBuyPanel;
    public GameObject buyMark;

    public GameObject buyButton;

    private TextMeshProUGUI officerBuyText;
    private TextMeshProUGUI officerLevelText;
    private TextMeshProUGUI refillTimeText;
    private TextMeshProUGUI officerPriceText;
    [SerializeField] private OfficerLevelSystem[] officerLevelSystem;
    private Image officerSprite;

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
        /*ini buat panel upgrade-------*/
        upgradeButton = upgradePanel.transform.GetChild(1).GetChild(1).gameObject;
        upgradeLevelText = upgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        upgradeRefillRateText = upgradeButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        upgradePriceText = upgradeButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        upgradeSprite = upgradeButton.transform.GetChild(6).GetComponent<Image>();
        /*-----------------------------*/

        officerBuyText = buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        officerLevelText = buyButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        refillTimeText = buyButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        officerPriceText = buyButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        officerSprite = buyButton.transform.GetChild(4).GetComponent<Image>();
        officerPriceText.text = "Price: " + price;
    }

    public void ShowBuyOfficerPanel(bool show) //munculin buy panel
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        officerBuyPanel.SetActive(show);
    }

    public void BuyOfficer()
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
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
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }
        else
        {
            Goverment.instance.Money -= price;
        }
        
        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        buyMark.SetActive(false);
        GameObject go = Instantiate(officerObj, officerPoint.transform.position, officerPoint.transform.rotation) as GameObject;
        go.transform.parent = officerPoint.transform;
        go.GetComponent<Officer>().AssignLevelSystem(officerLevelSystem);
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
        officerSprite.sprite = officer.GetSprite();
    }

    public void UpgradeAllAttribute()
    {
        if (officer.CheckMaxLevel()) return;

        Debug.Log("upgrade price: " + officer.UpgradePrice);
        if (Goverment.instance.Money >= officer.UpgradePrice)
        {
            Goverment.instance.Money -= officer.UpgradePrice;
        }
        else
        {
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }
        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        officer.UpgradeOfficer();
        UpdateBuyUI();
        upgradePanel.SetActive(false);
    }

    public void UpgradeOfficer()
    {
        if (officer.CheckMaxLevel()) return;

        int lvl = officer.Level;
        upgradePanel.SetActive(true);

        upgradeLevelText.text = "level: " + ((int)officer.Level + 1);

        upgradeRefillRateText.text = "Refill Rate: " + officer.GetNextValue(lvl).refillTime;
        upgradePriceText.text = "Price: " + officer.GetNextValue(lvl).price;
        upgradeSprite.sprite = officer.GetNextValue(lvl).sprite;
    }
}
