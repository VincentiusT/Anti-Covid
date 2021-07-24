using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmbulanceManager : MonoBehaviour
{
    public GameObject ambulanceObj;
    public GameObject buyAmbulancePanel;
    public GameObject[] buyButtons;

    private TextMeshProUGUI[] ambulanceBuyText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] ambulanceLevelText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] pickupRate = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] pickupTime = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] ambulancePriceText = new TextMeshProUGUI[4];
    

    //private List<Ambulance> ambulances;
    private Ambulance[] ambulances = { null, null, null, null };

    private int price = 15;

    private bool[] alreadyBought = { false, false, false, false };

    private void Start()
    {
        //ambulances = new List<Ambulance>();

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
        buyAmbulancePanel.SetActive(show);
    }

    public void BuyAmbulance(int whichAMbulance)
    {
        if (HospitalManager.instance.placeCount() < 1) return;

        if (alreadyBought[whichAMbulance])
        {
            UpgradeAmbulance(whichAMbulance);
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
        GameObject go = Instantiate(ambulanceObj) as GameObject;
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

    public void UpgradeAmbulance(int whichAmbulance)
    {
        if (ambulances[whichAmbulance].CheckMaxLevel()) return;

        Debug.Log("upgrade price: " + ambulances[whichAmbulance].UpgradePrice);
        if (Goverment.instance.Money >= ambulances[whichAmbulance].UpgradePrice)
        {
            Goverment.instance.Money -= ambulances[whichAmbulance].UpgradePrice;
        }
        else
        {
            return;
        }

        ambulances[whichAmbulance].UpgradePharmacy();
        UpdateBuyUI(whichAmbulance);
    }
}
