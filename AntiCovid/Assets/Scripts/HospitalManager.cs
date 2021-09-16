using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    public static HospitalManager instance;

    public GameObject upgradePanel;
    private GameObject upgradeButton;
    private TextMeshProUGUI upgradeLevelText, upgradeCapacityText, upgradeReleaseTimeText, upgradeRestTimeText, upgradePriceText;
    private Image upgradeSprite;
    private int currentSelected;

    public GameObject[] hospitalPoints;
    public GameObject hospitalObj;
    public GameObject hospitalBuyPanel;
    public GameObject[] buyButtons;
    public GameObject buyMark;

    private TextMeshProUGUI[] hospitalBuyText ;
    private TextMeshProUGUI[] hospitalLevelText ;
    private TextMeshProUGUI[] hospitalCapacityText;
    private TextMeshProUGUI[] hospitalRestTimeText;
    private TextMeshProUGUI[] hospitalReleaseCountText;
    private TextMeshProUGUI[] hospitalPriceText;
    private Image[] hospitalSprites;
    [SerializeField] private HospitalLevelSystem[] hospitalLevelSystem;

    [SerializeField] private int price = 30;
    //private List<Hospital> hospitals;
    public Hospital[] hospitals;
    private int totalHospitalizedPeoples;
    private bool[] alreadyBought;

    int index=0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        /*ini buat panel upgrade-------*/
        upgradeButton = upgradePanel.transform.GetChild(1).GetChild(1).gameObject;
        upgradeLevelText = upgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        upgradeCapacityText = upgradeButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        upgradeReleaseTimeText = upgradeButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        upgradeRestTimeText = upgradeButton.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        upgradePriceText = upgradeButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        upgradeSprite = upgradeButton.transform.GetChild(6).GetComponent<Image>();
        /*-----------------------------*/


        //hospitals = new List<Hospital>();
        hospitalBuyText = new TextMeshProUGUI[buyButtons.Length];
        hospitalLevelText = new TextMeshProUGUI[buyButtons.Length];
        hospitalCapacityText = new TextMeshProUGUI[buyButtons.Length];
        hospitalRestTimeText = new TextMeshProUGUI[buyButtons.Length];
        hospitalReleaseCountText = new TextMeshProUGUI[buyButtons.Length];
        hospitalPriceText = new TextMeshProUGUI[buyButtons.Length];
        hospitalSprites = new Image[buyButtons.Length];

        hospitals = new Hospital[buyButtons.Length];
        alreadyBought = new bool[buyButtons.Length];

        for (int i = 0; i < buyButtons.Length; i++)
        {
            hospitalBuyText[i] = buyButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            hospitalLevelText[i] = buyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            hospitalCapacityText[i] = buyButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            hospitalReleaseCountText[i] = buyButtons[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            hospitalRestTimeText[i] = buyButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            hospitalPriceText[i] = buyButtons[i].transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            hospitalSprites[i] = buyButtons[i].transform.GetChild(6).GetComponent<Image>();
            hospitalPriceText[i].text = "Price: " + price;
        }
    }

    public void HospitalizePeople(float multiplier, float speedupMultiplier) //masukin orang kerumah sakit pas di tap
    {
        int x=0;
        for(int i = 0; i < hospitals.Length; i++)
        {
            if(hospitals[i] == null)
            {
                x++;
            }
        }
        if (x >= hospitals.Length) return;
        while (hospitals[index] == null)
        {
            index++;
            if (index >= hospitals.Length)
            {
                index = 0;
            }
        }
        hospitals[index].ReceiveSickPeople((int)(hospitals[index].PeopleOutPerTap * speedupMultiplier + hospitals[index].PeopleOutPerTap * (multiplier - 1)));
        index++;
        if (index >= hospitals.Length)
        {
            index = 0;
        }
    }

    public void HospitalizePeopleFromAmbulance(int peoples)
    {
        if (hospitals.Length <= 0) return;
        while (hospitals[index] == null)
        {
            index++;
            if (index >= hospitals.Length)
            {
                index = 0;
            }
        }
        hospitals[index].ReceiveSickPeople(peoples);
        index++;
        if (index >= hospitals.Length)
        {
            index = 0;
        }
    }

    public int GetAllHospitalizePeople() //get semua orang yang ada di semua rumah sakit
    {
        totalHospitalizedPeoples = 0;
        for(int i=0;i< hospitals.Length; i++)
        {
            if(hospitals[i]!=null)
            totalHospitalizedPeoples += hospitals[i].GetHospitalizePeople();
        }

        return totalHospitalizedPeoples;
    }

    public void ShowBuyHospitalPanel(bool show) //munculin buy panel
    {
        if (!show && Tutorial.instance.IsBuyTutorial) return;
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        hospitalBuyPanel.SetActive(show);
    }
    
    public void RebuildHospital(int whichHospital, int _level)
    {
        if (_level == 0) return;

        BuildHospital(whichHospital, true);
        for (int i = 0; i < _level - 1; i++)
        {
            UpgradeHospital(whichHospital);
            UpgradeAllAttribute(true);
        }
    }

    public void BuyHospital(int whichHospital)
    {
        if (Tutorial.instance.IsBuyTutorial)
        {
            Tutorial.instance.IsBuyTutorial = false;
            StartCoroutine(Tutorial.instance.StopHospitalTutorial());
        }
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        

        BuildHospital(whichHospital);
    }

    private void BuildHospital(int whichHospital, bool rebuild = false)
    {
        if (alreadyBought[whichHospital]) UpgradeHospital(whichHospital);

        if (hospitalPoints[whichHospital].transform.childCount >= 1) return;

        if (!rebuild)
        {
            if (Goverment.instance.Money < price)
            {
                UIManager.instance.ShowNotifPanel("you don't have enough money");
                return;
            }
            else Goverment.instance.Money -= price;
        }

        if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        buyMark.SetActive(false);
        GameObject go = Instantiate(hospitalObj, hospitalPoints[whichHospital].transform.position, hospitalPoints[whichHospital].transform.rotation) as GameObject;
        go.GetComponent<Hospital>().AssignLevelSystem(hospitalLevelSystem);
        go.transform.parent = hospitalPoints[whichHospital].transform;
        go.name = "hospital" + whichHospital;
        if (whichHospital == 0)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        else if (whichHospital == 3)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else if (whichHospital == 1)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else if (whichHospital == 2)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }

        hospitals[whichHospital] = go.GetComponent<Hospital>();

        ShowBuyHospitalPanel(false);

        alreadyBought[whichHospital] = true;
        UpdateBuyUI(whichHospital);
    }

    private void UpdateBuyUI(int index)
    {
        if (hospitals[index].CheckMaxLevel())
        {
            hospitalLevelText[index].text = "level: MAX";
            buyButtons[index].GetComponent<Button>().interactable = false;
        }
        else
        {
            hospitalLevelText[index].text = "level: " + hospitals[index].Level;
            hospitalBuyText[index].text = "Upgrade";
        }
        hospitalCapacityText[index].text = "Capacity: " + hospitals[index].Capacity;
        hospitalReleaseCountText[index].text = "Release Count: " + hospitals[index].ReleaseCount;
        hospitalRestTimeText[index].text = "Rest Time: " + hospitals[index].RestTime;
        hospitalPriceText[index].text = "Price: " + hospitals[index].UpgradePrice;
        hospitalSprites[index].sprite = hospitals[index].GetSprite();
    }

    public void UpgradeAllAttribute(bool rebuild = false)
    {
        int whichHospital = currentSelected;
        if (hospitals[whichHospital].CheckMaxLevel()) return;

        if (!rebuild)
        {
            if (Goverment.instance.Money >= hospitals[whichHospital].UpgradePrice)
            {
                Goverment.instance.Money -= hospitals[whichHospital].UpgradePrice;
            }
            else
            {
                UIManager.instance.ShowNotifPanel("you don't have enough money");
                return;
            }
            if (AudioManager.instance != null) AudioManager.instance.Play("construct");
        }

        hospitals[whichHospital].UpgradeHospital();
        UpdateBuyUI(whichHospital);
        upgradePanel.SetActive(false);
    }

    public void UpgradeHospital(int whichHospital)
    {
        if (hospitals[whichHospital].CheckMaxLevel()) return;

        int lvl = hospitals[whichHospital].Level;
        currentSelected = whichHospital;
        upgradePanel.SetActive(true);

        upgradeLevelText.text = "level: " + ((int)hospitals[whichHospital].Level+1);
        
        upgradeCapacityText.text = "Capacity: " + hospitals[whichHospital].GetNextValue(lvl).capacity;
        upgradeReleaseTimeText.text = "Release Count: " + hospitals[whichHospital].GetNextValue(lvl).outRate;
        upgradeRestTimeText.text = "Rest Time: " + hospitals[whichHospital].GetNextValue(lvl).outSpeed;
        upgradePriceText.text = "Price: " + hospitals[whichHospital].GetNextValue(lvl).price;
        upgradeSprite.sprite = hospitals[whichHospital].GetNextValue(lvl).sprite;
    }

    public int placeCount()
    {
        int counter = 0;
        for (int i = 0; i < hospitals.Length; i++)
        {
            if (hospitals[i] != null) counter++;
        }
        return counter;
    }

    public void BuyTutorial()
    {

    }
}
