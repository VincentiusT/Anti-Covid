using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceManager : MonoBehaviour
{
    public GameObject ambulanceObj;
    public GameObject buyAmbulancePanel;

    private int price = 15;
    public void ShowAmbulanceBuyPanel(bool show)
    {
        buyAmbulancePanel.SetActive(show);
    }

    public void BuyAmbulance()
    {
        if (Goverment.instance.Money < price)
        {
            return;
        }
        else
        {
            Goverment.instance.Money -= price;
        }
        GameObject go = Instantiate(ambulanceObj) as GameObject;
        go.transform.parent = transform;
    }
}
