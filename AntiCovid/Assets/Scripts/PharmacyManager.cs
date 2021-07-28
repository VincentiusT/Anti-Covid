using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PharmacyManager : MonoBehaviour
{
    public static PharmacyManager instance;

    public GameObject[] pharmacyPoints;
    public GameObject pharmacyObj;
    public GameObject pharmacyBuyPanel;
    public GameObject[] buyButtons;

    private TextMeshProUGUI[] pharmacyBuyText;
    private TextMeshProUGUI[] pharmacyLevelText;
    private TextMeshProUGUI[] transmissionDecreaseRateText;
    private TextMeshProUGUI[] pharmacyPriceText;

    public GameObject buyMark;

    private Image[] pharmacySprites;
    [SerializeField] private int price=20;
    private Pharmacy[] pharmacy;
    private bool[] alreadyBought;

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pharmacyBuyText = new TextMeshProUGUI[buyButtons.Length];
        pharmacyLevelText = new TextMeshProUGUI[buyButtons.Length];
        transmissionDecreaseRateText = new TextMeshProUGUI[buyButtons.Length];
        pharmacyPriceText = new TextMeshProUGUI[buyButtons.Length];
        pharmacySprites = new Image[buyButtons.Length];

        pharmacy = new Pharmacy[buyButtons.Length];
        alreadyBought = new bool[buyButtons.Length];

        for (int i = 0; i < buyButtons.Length; i++)
        {
            pharmacyBuyText[i] = buyButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            pharmacyLevelText[i] = buyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            transmissionDecreaseRateText[i] = buyButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            pharmacyPriceText[i] = buyButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            pharmacySprites[i] = buyButtons[i].transform.GetChild(4).GetComponent<Image>();
            pharmacyPriceText[i].text = "Price: " + price;
        }
    }

    public void ShowBuyPharmacyPanel(bool show) //munculin buy panel
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        pharmacyBuyPanel.SetActive(show);
    }

    public void BuyPharmacy(int whichPharmacy)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        if (alreadyBought[whichPharmacy])
        {
            UpgradePharmacy(whichPharmacy);
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
        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        buyMark.SetActive(false);
        GameObject go = Instantiate(pharmacyObj, pharmacyPoints[whichPharmacy].transform.position, pharmacyPoints[whichPharmacy].transform.rotation) as GameObject;
        go.transform.parent = pharmacyPoints[whichPharmacy].transform;
        go.name = "pharmacy" + whichPharmacy;
        if (whichPharmacy == 0)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (whichPharmacy == 1)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (whichPharmacy == 2)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if(whichPharmacy == 3)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        //pharmacy.Add(go.GetComponent<Pharmacy>());
        pharmacy[whichPharmacy] = go.GetComponent<Pharmacy>();

        ShowBuyPharmacyPanel(false);
        alreadyBought[whichPharmacy] = true;
        UpdateBuyUI(whichPharmacy);
    }

    private void UpdateBuyUI(int index)
    {
        if (pharmacy[index].CheckMaxLevel())
        {
            pharmacyLevelText[index].text = "level: MAX";
            buyButtons[index].GetComponent<Button>().interactable = false;
        }
        else
        {
            pharmacyLevelText[index].text = "level: " + pharmacy[index].Level;
            pharmacyBuyText[index].text = "Upgrade";
        }
        transmissionDecreaseRateText[index].text = "Transmission decrease rate: " + pharmacy[index].TransmissionDecreaseRate;
        pharmacyPriceText[index].text = "Price: " + pharmacy[index].UpgradePrice;
        pharmacySprites[index].sprite = pharmacy[index].GetSprite();
    }

    public void UpgradePharmacy(int whichPharmacy)
    {
        if (pharmacy[whichPharmacy].CheckMaxLevel()) return;

        Debug.Log("upgrade price: " + pharmacy[whichPharmacy].UpgradePrice);
        if (Goverment.instance.Money >= pharmacy[whichPharmacy].UpgradePrice)
        {
            Goverment.instance.Money -= pharmacy[whichPharmacy].UpgradePrice;
        }
        else
        {
            return;
        }
        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        pharmacy[whichPharmacy].UpgradePharmacy();
        UpdateBuyUI(whichPharmacy);
    }

    public int placeCount()
    {
        int counter = 0;
        for (int i = 0; i < pharmacy.Length; i++)
        {
            if (pharmacy[i] != null) counter++;
        }
        return counter;
    }

}
