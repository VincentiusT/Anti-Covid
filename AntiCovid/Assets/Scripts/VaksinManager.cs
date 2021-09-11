using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VaksinManager : MonoBehaviour
{
    public static VaksinManager instance;

    public int hospitalNeeded=1, pharmacyNeeded=1;

    public GameObject upgradePanel;
    private GameObject upgradeButton;
    private TextMeshProUGUI upgradeLevelText, upgradeVaccineRate, upgradeVaccineTime, upgradePriceText;
    private Image upgradeSprite;
    private int currentSelected;

    public GameObject[] vaksinPlacePoints;
    public GameObject vaksinPlaceObj;
    public GameObject vaksinPlaceBuyPanel;
    public GameObject[] buyButtons;
    public GameObject buyMark;

    private TextMeshProUGUI infoText;

    private TextMeshProUGUI[] vaccinePlaceBuyText;
    private TextMeshProUGUI[] vaccinePlaceLevelText;
    private TextMeshProUGUI[] vaccineRateText;
    private TextMeshProUGUI[] vaccineTimeText;
    private TextMeshProUGUI[] vaccinePlacePriceText;
    [SerializeField] private VaccineLevelSystem[] vaccineLevelSystems;
    [SerializeField]private TextMeshProUGUI vaccineStockText;

    [SerializeField] private int price = 15;
    //private List<VaksinPlace> vaksinPlace;
    private int vaccineStock = 0;

    private bool[] alreadyBought;
    private VaksinPlace[] vaksinPlace;

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
        upgradeVaccineRate = upgradeButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        upgradeVaccineTime = upgradeButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        upgradePriceText = upgradeButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        upgradeSprite = upgradeButton.transform.GetChild(6).GetComponent<Image>();
        /*-----------------------------*/

        infoText = vaksinPlaceBuyPanel.transform.Find("centerPanel/vaccinePanel/info/info").GetComponent<TextMeshProUGUI>();
        infoText.text = "A Place to vaccinate people! \nrequirement: You need to have minimal "+hospitalNeeded+" hospitals, "+pharmacyNeeded+" pharmacies to build this Place.";

        vaccinePlaceBuyText = new TextMeshProUGUI[buyButtons.Length];
        vaccinePlaceLevelText = new TextMeshProUGUI[buyButtons.Length];
        vaccineRateText = new TextMeshProUGUI[buyButtons.Length];
        vaccineTimeText = new TextMeshProUGUI[buyButtons.Length];
        vaccinePlacePriceText = new TextMeshProUGUI[buyButtons.Length];

        vaksinPlace = new VaksinPlace[buyButtons.Length];
        alreadyBought = new bool[buyButtons.Length];

        for (int i = 0; i < buyButtons.Length; i++)
        {
            vaccinePlaceBuyText[i] = buyButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            vaccinePlaceLevelText[i] = buyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            vaccineRateText[i] = buyButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            vaccineTimeText[i] = buyButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            vaccinePlacePriceText[i] = buyButtons[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            vaccinePlacePriceText[i].text = "Price: " + price;
        }
    }

    private void FixedUpdate()
    {
        vaccineStockText.text = vaccineStock.ToString("0");
        UIManager.instance.UpdateVaccineStockUI(vaccineStock);
    }

    public void ShowBuyVaksinPlacePanel(bool show) //munculin buy panel
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        vaksinPlaceBuyPanel.SetActive(show);
    }

    public void BuyVaksinPlace(int whichVaksinPlace)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");

        if (HospitalManager.instance.placeCount() < hospitalNeeded || (PharmacyManager.instance!=null && PharmacyManager.instance.placeCount() < pharmacyNeeded))
        {
            string msg;
            if (PharmacyManager.instance == null) msg = "You need to have at least " + hospitalNeeded + " hospitals to buy vaccine place";
            else msg = "You need to have at least " + hospitalNeeded + " hospitals and " + pharmacyNeeded + " pharmacy to buy vaccine place";
            UIManager.instance.ShowNotifPanel(msg);
            return;
        }
        if (alreadyBought[whichVaksinPlace])
        {
            UpgradeVaksinPlace(whichVaksinPlace);
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
        GameObject go = Instantiate(vaksinPlaceObj, vaksinPlacePoints[whichVaksinPlace].transform.position, vaksinPlacePoints[whichVaksinPlace].transform.rotation) as GameObject;
        go.transform.parent = vaksinPlacePoints[whichVaksinPlace].transform;
        go.GetComponent<VaksinPlace>().AssignLevelSystem(vaccineLevelSystems);
        go.name = "vaksinPlace" + whichVaksinPlace;
        if (whichVaksinPlace == 0)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (whichVaksinPlace == 1)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (whichVaksinPlace == 2)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (whichVaksinPlace == 3)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        //vaksinPlace.Add(go.GetComponent<VaksinPlace>());
        vaksinPlace[whichVaksinPlace] = go.GetComponent<VaksinPlace>();

        ShowBuyVaksinPlacePanel(false);
        alreadyBought[whichVaksinPlace] = true;
        UpdateBuyUI(whichVaksinPlace);
    }

    private void UpdateBuyUI(int index)
    {
        if (vaksinPlace[index].CheckMaxLevel())
        {
            vaccinePlaceLevelText[index].text = "level: MAX";
            buyButtons[index].GetComponent<Button>().interactable = false;
        }
        else
        {
            vaccinePlaceLevelText[index].text = "level: " + vaksinPlace[index].Level;
            vaccinePlaceBuyText[index].text = "Upgrade";
        }
        vaccineRateText[index].text = "Vaksin Rate: " + vaksinPlace[index].VaksinRate;
        vaccineTimeText[index].text = "Vaksin Time: " + vaksinPlace[index].VaksinTime;
        vaccinePlacePriceText[index].text = "Price: " + vaksinPlace[index].UpgradePrice;
    }

    public void UpgradeAllAttribute()
    {
        int whichVaksinPlace = currentSelected;
        if (vaksinPlace[whichVaksinPlace].CheckMaxLevel())
        {
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }

        if (Goverment.instance.Money >= vaksinPlace[whichVaksinPlace].UpgradePrice)
        {
            Goverment.instance.Money -= vaksinPlace[whichVaksinPlace].UpgradePrice;
        }
        else
        {
            UIManager.instance.ShowNotifPanel("you don't have enough money");
            return;
        }
        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        vaksinPlace[whichVaksinPlace].UpgradeVaksinPlace();
        UpdateBuyUI(whichVaksinPlace);
        upgradePanel.SetActive(false);
    }
    public void UpgradeVaksinPlace(int whichVaksinPlace)
    {
        if (vaksinPlace[whichVaksinPlace].CheckMaxLevel()) return;

        int lvl = vaksinPlace[whichVaksinPlace].Level;
        currentSelected = whichVaksinPlace;
        upgradePanel.SetActive(true);

        upgradeLevelText.text = "level: " + ((int)vaksinPlace[whichVaksinPlace].Level + 1);

        upgradeVaccineRate.text = "Vaccination Rate: " + vaksinPlace[whichVaksinPlace].GetNextValue(lvl).vaksinRate;
        upgradeVaccineTime.text = "Vaccination Time: " + vaksinPlace[whichVaksinPlace].GetNextValue(lvl).vaksinTime;
        upgradePriceText.text = "Price: " + vaksinPlace[whichVaksinPlace].GetNextValue(lvl).price;
    }

    public int placeCount()
    {
        int counter=0;
        for(int i = 0; i < vaksinPlace.Length; i++) 
        {
            if (vaksinPlace[i] != null) counter++;
        }
        return counter;
    }

    public int VaccineStock
    {
        get { return vaccineStock; }
        set { vaccineStock = value; }
    }
}
