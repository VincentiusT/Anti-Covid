using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacyManager : MonoBehaviour
{
    public static PharmacyManager instance;

    public GameObject[] pharmacyPoints;
    public GameObject pharmacyObj;
    public GameObject pharmacyBuyPanel;

    private int price;
    private List<Pharmacy> pharmacy;

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pharmacy = new List<Pharmacy>();

        for (int i = 0; i < pharmacyPoints.Length; i++)
        {
            pharmacy.Add(pharmacyPoints[i].GetComponentInChildren<Pharmacy>());
            Debug.Log("initiate " + pharmacy[i]);
        }
    }

    public void ShowBuyPharmacyPanel(bool show) //munculin buy panel
    {
        pharmacyBuyPanel.SetActive(show);
    }

    public void BuyPharmacy(int whichPharmacy)
    {
        if(pharmacyPoints[whichPharmacy].transform.childCount >= 1)//klo udh beli lgsg return
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
        pharmacy.Add(go.GetComponent<Pharmacy>());

        ShowBuyPharmacyPanel(false);
    }
}
