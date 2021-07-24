using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    public static HospitalManager instance;

    public GameObject[] hospitalPoints;
    public GameObject hospitalObj;
    public GameObject hospitalBuyPanel;
    public GameObject[] buyButtons;

    private TextMeshProUGUI[] hospitalBuyText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] hospitalLevelText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] hospitalCapacityText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] hospitalRestTimeText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] hospitalReleaseCountText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] hospitalPriceText = new TextMeshProUGUI[4];
    [SerializeField] private int price = 30;
    //private List<Hospital> hospitals;
    private Hospital[] hospitals = { null, null, null, null};
    private int totalHospitalizedPeoples;
    private bool[] alreadyBought = { false, false, false, false};

    int index=0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //hospitals = new List<Hospital>();
        for(int i = 0; i < buyButtons.Length; i++)
        {
            hospitalBuyText[i] = buyButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            hospitalLevelText[i] = buyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            hospitalCapacityText[i] = buyButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            hospitalReleaseCountText[i] = buyButtons[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            hospitalRestTimeText[i] = buyButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            hospitalPriceText[i] = buyButtons[i].transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            hospitalPriceText[i].text = "Price: " + price;
        }
    }

    public void HospitalizePeople(float multiplier) //masukin orang kerumah sakit pas di tap
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
        hospitals[index].ReceiveSickPeople((int)(hospitals[index].PeopleOutPerTap * multiplier));
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
        hospitalBuyPanel.SetActive(show);
    }

    public void BuyHospital(int whichHospital)
    {
        if (alreadyBought[whichHospital]) UpgradeHospital(whichHospital);
        
        if(hospitalPoints[whichHospital].transform.childCount >= 1) return;
        
        if(Goverment.instance.Money < price) return;
        else Goverment.instance.Money -= price;


        GameObject go = Instantiate(hospitalObj, hospitalPoints[whichHospital].transform.position, hospitalPoints[whichHospital].transform.rotation) as GameObject;
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
    }

    public void UpgradeHospital(int whichHospital)
    {
        if (hospitals[whichHospital].CheckMaxLevel()) return;

        if (Goverment.instance.Money >= hospitals[whichHospital].UpgradePrice)
        {
            Goverment.instance.Money -= hospitals[whichHospital].UpgradePrice;
        }
        else
        {
            return;
        }

        hospitals[whichHospital].UpgradeHospital();
        UpdateBuyUI(whichHospital);


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
}
