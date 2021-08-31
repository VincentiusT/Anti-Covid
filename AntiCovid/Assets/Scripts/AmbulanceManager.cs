using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmbulanceManager : MonoBehaviour
{
    public static AmbulanceManager instance;

    public GameObject upgradePanel;
    private GameObject upgradeButton;
    private TextMeshProUGUI upgradeLevelText, upgradePickUpRateText, upgradeRPickUpTimeText, upgradePriceText;
    private Image upgradeSprite;
    private int currentSelected;

    public GameObject ambulanceObj;
    public GameObject buyAmbulancePanel;
    public GameObject[] buyButtons;

    private TextMeshProUGUI[] ambulanceBuyText;
    private TextMeshProUGUI[] ambulanceLevelText;
    private TextMeshProUGUI[] pickupRate;
    private TextMeshProUGUI[] pickupTime;
    private TextMeshProUGUI[] ambulancePriceText;
    [SerializeField] private AmbulanceLevelSystem[] ambulanceLevelSystem;

    //private List<Ambulance> ambulances;
    private Ambulance[] ambulances;

    [SerializeField] private int price = 15;

    private bool[] alreadyBought ;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        /*ini buat panel upgrade-------*/
        upgradeButton = upgradePanel.transform.GetChild(1).GetChild(1).gameObject;
        upgradeLevelText = upgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        upgradePickUpRateText = upgradeButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        upgradeRPickUpTimeText = upgradeButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        upgradePriceText = upgradeButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        upgradeSprite = upgradeButton.transform.GetChild(6).GetComponent<Image>();
        /*-----------------------------*/

        ambulanceBuyText = new TextMeshProUGUI[buyButtons.Length];
        ambulanceLevelText = new TextMeshProUGUI[buyButtons.Length];
        pickupRate = new TextMeshProUGUI[buyButtons.Length];
        pickupTime = new TextMeshProUGUI[buyButtons.Length];
        ambulancePriceText = new TextMeshProUGUI[buyButtons.Length];

        ambulances = new Ambulance[buyButtons.Length];
        alreadyBought = new bool[buyButtons.Length];

        for (int i = 0; i < buyButtons.Length; i++)
        {
            ambulanceBuyText[i] = buyButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            ambulanceLevelText[i] = buyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            pickupRate[i] = buyButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            pickupTime[i] = buyButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            ambulancePriceText[i] = buyButtons[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            ambulancePriceText[i].text = "Price: " + price;
        }
    }

    public void ShowAmbulanceBuyPanel(bool show)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        buyAmbulancePanel.SetActive(show);
    }

    public void BuyAmbulance(int whichAMbulance)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        if (HospitalManager.instance.placeCount() < 1)
        {
            UIManager.instance.ShowNotifPanel("you need to have at least 1 hospital to buy ambulance");
            return;
        }
        if (alreadyBought[whichAMbulance])
        {
            UpgradeAmbulance(whichAMbulance);
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
        GameObject go = Instantiate(ambulanceObj) as GameObject;
        go.GetComponent<Ambulance>().AssignLevelSystem(ambulanceLevelSystem);
        int randSpawnPoint = Random.Range(0, transform.childCount);
        go.transform.parent = transform;

        //ambulances.Add(go.GetComponent<Ambulance>());
        ambulances[whichAMbulance] = go.GetComponent<Ambulance>();

        alreadyBought[whichAMbulance] = true;
        UpdateBuyUI(whichAMbulance);
    }

    private void UpdateBuyUI(int index)
    {
        if (ambulances[index].CheckMaxLevel())
        {
            ambulanceLevelText[index].text = "level: MAX";
            buyButtons[index].GetComponent<Button>().interactable = false;
        }
        else
        {
            ambulanceLevelText[index].text = "level: " + ambulances[index].Level;
            ambulanceBuyText[index].text = "Upgrade";
        }
        pickupRate[index].text = "Pickup Rate : " + ambulances[index].PickupRate;
        pickupTime[index].text = "Pickup Time : " + ambulances[index].PickupTime;

        ambulancePriceText[index].text = "Price: " + ambulances[index].UpgradePrice;
    }

    public void UpgradeAllAttribute()
    {
        int whichAmbulance = currentSelected;
        if (ambulances[whichAmbulance].CheckMaxLevel())
        {
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }

        if (Goverment.instance.Money >= ambulances[whichAmbulance].UpgradePrice)
        {
            Goverment.instance.Money -= ambulances[whichAmbulance].UpgradePrice;
        }
        else
        {
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }

        ambulances[whichAmbulance].UpgradePharmacy();
        UpdateBuyUI(whichAmbulance);
        upgradePanel.SetActive(false);
    }

    public void UpgradeAmbulance(int whichAmbulance)
    {
        if (ambulances[whichAmbulance].CheckMaxLevel()) return;

        int lvl = ambulances[whichAmbulance].Level;
        currentSelected = whichAmbulance;
        upgradePanel.SetActive(true);

        upgradeLevelText.text = "level: " + ((int)ambulances[whichAmbulance].Level + 1);

        upgradePickUpRateText.text = "Pickup Rate: " + ambulances[whichAmbulance].GetNextValue(lvl).pickupRate;
        upgradeRPickUpTimeText.text = "Pickup Time: " + ambulances[whichAmbulance].GetNextValue(lvl).pickupTime + " ~ " + ambulances[whichAmbulance].GetNextValue(lvl).pickupTimeMax;
        upgradePriceText.text = "Price: " + ambulances[whichAmbulance].GetNextValue(lvl).price;
    }
}
