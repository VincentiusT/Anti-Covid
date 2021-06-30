using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerManager : MonoBehaviour
{
    public static OfficerManager instance;

    public GameObject officerPoint;
    public GameObject officerObj;
    public GameObject officerBuyPanel;

    private int price = 10;
    private Officer officer;

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }

    public void ShowBuyOfficerPanel(bool show) //munculin buy panel
    {
        officerBuyPanel.SetActive(show);
    }

    public void BuyOfficer()
    {
        if (officerPoint.transform.childCount >= 1)//klo udh beli lgsg return
        {
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
        GameObject go = Instantiate(officerObj, officerPoint.transform.position, officerPoint.transform.rotation) as GameObject;
        go.transform.parent = officerPoint.transform;
        go.name = "Officer";

        officer = go.GetComponent<Officer>();

        ShowBuyOfficerPanel(false);
    }
}
