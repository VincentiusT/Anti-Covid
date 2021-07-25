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

    private TextMeshProUGUI[] vaccinePlaceBuyText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] vaccinePlaceLevelText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] vaccineRateText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] vaccineTimeText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] vaccinePlacePriceText = new TextMeshProUGUI[4];

    [SerializeField] private int price = 15;
    //private List<VaksinPlace> vaksinPlace;

    private bool[] alreadyBought = { false, false, false, false };
    private VaksinPlace[] vaksinPlace = { null, null, null, null };

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //vaksinPlace = new List<VaksinPlace>();

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

    public void ShowBuyVaksinPlacePanel(bool show) //munculin buy panel
    {
        vaksinPlaceBuyPanel.SetActive(show);
    }

    public void BuyVaksinPlace(int whichVaksinPlace)
    {
        if (alreadyBought[whichVaksinPlace])
        {
            UpgradeVaksinPlace(whichVaksinPlace);
            return;
        }

        if (HospitalManager.instance.placeCount() < 3 && PharmacyManager.instance.placeCount() < 3) return;

        if (Goverment.instance.Money < price)
        {
            return;
        }
        else
        {
            Goverment.instance.Money -= price;
        }
        GameObject go = Instantiate(vaksinPlaceObj, vaksinPlacePoints[whichVaksinPlace].transform.position, vaksinPlacePoints[whichVaksinPlace].transform.rotation) as GameObject;
        go.transform.parent = vaksinPlacePoints[whichVaksinPlace].transform;
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
        if (vaksinPlace[whichVaksinPlace].CheckMaxLevel()) return;

        if (Goverment.instance.Money >= vaksinPlace[whichVaksinPlace].UpgradePrice)
        {
            Goverment.instance.Money -= vaksinPlace[whichVaksinPlace].UpgradePrice;
        }
        else
        {
            return;
        }

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
}
