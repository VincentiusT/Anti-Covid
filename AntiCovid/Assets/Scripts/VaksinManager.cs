using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VaksinManager : MonoBehaviour
{
    public static VaksinManager instance;

    public GameObject[] vaksinPlacePoints;
    public GameObject vaksinPlaceObj;
    public GameObject vaksinPlaceBuyPanel;
    public GameObject[] buyButtons;
    public GameObject buyMark;

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

    private void Update()
    {
        vaccineStockText.text = vaccineStock.ToString("0");
    }

    public void ShowBuyVaksinPlacePanel(bool show) //munculin buy panel
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        vaksinPlaceBuyPanel.SetActive(show);
    }

    public void BuyVaksinPlace(int whichVaksinPlace)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        if (HospitalManager.instance.placeCount() < 2 || PharmacyManager.instance.placeCount() < 2)
        {
            UIManager.instance.ShowNotifPanel("You need to have at least 3 hospitals and 3 pharmacy to buy vaccine place");
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

    public void UpgradeVaksinPlace(int whichVaksinPlace)
    {
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
