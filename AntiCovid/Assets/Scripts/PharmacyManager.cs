using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PharmacyManager : MonoBehaviour
{
    public static PharmacyManager instance;

    public GameObject upgradePanel;
    private GameObject upgradeButton;
    private TextMeshProUGUI upgradeLevelText, upgradeTDRText, upgradePriceText;
    private Image upgradeSprite;
    private int currentSelected;

    public GameObject[] pharmacyPoints;
    public GameObject pharmacyObj;
    public GameObject pharmacyBuyPanel;
    public GameObject[] buyButtons;

    private TextMeshProUGUI[] pharmacyBuyText;
    private TextMeshProUGUI[] pharmacyLevelText;
    private TextMeshProUGUI[] transmissionDecreaseRateText;
    private TextMeshProUGUI[] pharmacyPriceText;
    [SerializeField] private PharmacyLevelSystem[] pharmacyLevelSystem;

    public GameObject buyMark;

    private Image[] pharmacySprites;
    [SerializeField] private int price=20;
    public Pharmacy[] pharmacy;
    private bool[] alreadyBought;

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
        upgradeTDRText = upgradeButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        upgradePriceText = upgradeButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        upgradeSprite = upgradeButton.transform.GetChild(6).GetComponent<Image>();
        /*-----------------------------*/

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

    public void RebuildPharmacy(int whichPharmacy, int _level)
    {
        if (_level == 0) return;

        BuildPharmacy(whichPharmacy, true);
        for (int i = 0; i < _level - 1; i++)
        {
            UpgradePharmacy(whichPharmacy);
            UpgradeAllAttribute(true);
        }
    }

    public void BuyPharmacy(int whichPharmacy)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        if (alreadyBought[whichPharmacy])
        {
            UpgradePharmacy(whichPharmacy);
            return;
        }
        BuildPharmacy(whichPharmacy);
    }

    private void BuildPharmacy(int whichPharmacy, bool isRebuilding = false)
    {
        if (!isRebuilding)
        {
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
        }

        buyMark.SetActive(false);
        GameObject go = Instantiate(pharmacyObj, pharmacyPoints[whichPharmacy].transform.position, pharmacyPoints[whichPharmacy].transform.rotation) as GameObject;
        go.GetComponent<Pharmacy>().AssignLevelSystem(pharmacyLevelSystem);
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
        else if (whichPharmacy == 3)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        //pharmacy.Add(go.GetComponent<Pharmacy>());
        pharmacy[whichPharmacy] = go.GetComponent<Pharmacy>();

        if (pharmacyPoints[whichPharmacy].tag == "left")
        {
            go.GetComponent<SpriteRenderer>().flipX = true;
        }

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

    public void UpgradeAllAttribute(bool isRebuilding = false)
    {
        int whichPharmacy = currentSelected;
        if (pharmacy[whichPharmacy].CheckMaxLevel()) return;

        Debug.Log("upgrade price: " + pharmacy[whichPharmacy].UpgradePrice);
        if (!isRebuilding)
        {
            if (Goverment.instance.Money >= pharmacy[whichPharmacy].UpgradePrice)
            {
                Goverment.instance.Money -= pharmacy[whichPharmacy].UpgradePrice;
            }
            else
            {
                UIManager.instance.ShowNotifPanel("you don't have enough money");
                return;
            }
            if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        }

        pharmacy[whichPharmacy].UpgradePharmacy();
        UpdateBuyUI(whichPharmacy);
        upgradePanel.SetActive(false);
    }
    public void UpgradePharmacy(int whichPharmacy)
    {
        if (pharmacy[whichPharmacy].CheckMaxLevel()) return;

        int lvl = pharmacy[whichPharmacy].Level;
        currentSelected = whichPharmacy;
        upgradePanel.SetActive(true);

        upgradeLevelText.text = "level: " + ((int)pharmacy[whichPharmacy].Level + 1);

        upgradeTDRText.text = "Transmission\n Decrease Rate: " + pharmacy[whichPharmacy].GetNextValue(lvl).transmissionDecreaseRate;
        upgradePriceText.text = "Price: " + pharmacy[whichPharmacy].GetNextValue(lvl).price;
        upgradeSprite.sprite = pharmacy[whichPharmacy].GetNextValue(lvl).sprite;
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
