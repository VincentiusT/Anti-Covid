using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalManager : MonoBehaviour
{
    public static HospitalManager instance;

    public GameObject[] hospitalPoints;
    public GameObject hospitalObj;

    private List<Hospital> hospitals;
    private int totalHospitalizedPeoples;

    int index=0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        hospitals = new List<Hospital>();

        for (int i = 0; i < hospitalPoints.Length; i++)
        {
            hospitals.Add(hospitalPoints[i].GetComponentInChildren<Hospital>());
            Debug.Log("initiate " + hospitals[i]);
        }
    }
    public void HospitalizePeople() //masukin orang kerumah sakit pas di tap
    {
        hospitals[index].ReceiveSickPeople(1);
        index++;
        if (index >= hospitals.Count)
        {
            index = 0;
        }
    }

    public void HospitalizePeopleFromAmbulance(int peoples)
    {
        hospitals[index].ReceiveSickPeople(peoples);
        index++;
        if (index >= hospitals.Count)
        {
            index = 0;
        }
    }

    public int GetAllHospitalizePeople() //get semua orang yang ada di semua rumah sakit
    {
        totalHospitalizedPeoples = 0;
        for(int i=0;i< hospitals.Count; i++)
        {
            totalHospitalizedPeoples += hospitals[i].GetHospitalizePeople();
        }

        return totalHospitalizedPeoples;
    }

    public void ShowBuyHospitalPanel() //munculin buy panel
    {

    }

    public void BuyHospital(int whichHospital)
    {
        GameObject go = Instantiate(hospitalObj, hospitalPoints[whichHospital].transform.position, hospitalPoints[whichHospital].transform.rotation) as GameObject;
        go.transform.parent = hospitalPoints[whichHospital].transform;
        go.name = "hospital" + whichHospital;
        hospitals.Add(go.GetComponent<Hospital>());
    }
}
