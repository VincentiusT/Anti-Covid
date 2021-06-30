using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacyManager : MonoBehaviour
{
    public static PharmacyManager instance;

    public GameObject[] pharmacyPoints;
    public GameObject pharmacyObj;

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

    public void ShowBuyPharmacyPanel() //munculin buy panel
    {

    }

    public void BuyPharmacy(int whichPharmacy)
    {
        GameObject go = Instantiate(pharmacyObj, pharmacyPoints[whichPharmacy].transform.position, pharmacyPoints[whichPharmacy].transform.rotation) as GameObject;
        go.transform.parent = pharmacyPoints[whichPharmacy].transform;
        go.name = "pharmacy" + whichPharmacy;
        pharmacy.Add(go.GetComponent<Pharmacy>());
    }
}
