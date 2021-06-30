using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaksinManager : MonoBehaviour
{
    public static VaksinManager instance;

    public GameObject[] vaksinPlacePoints;
    public GameObject vaksinPlaceObj;
    public GameObject vaksinPlaceBuyPanel;

    private int price;
    private List<VaksinPlace> vaksinPlace;

    int index = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        vaksinPlace = new List<VaksinPlace>();

        for (int i = 0; i < vaksinPlacePoints.Length; i++)
        {
            vaksinPlace.Add(vaksinPlacePoints[i].GetComponentInChildren<VaksinPlace>());
            Debug.Log("initiate " + vaksinPlace[i]);
        }
    }

    public void ShowBuyVaksinPlacePanel(bool show) //munculin buy panel
    {
        vaksinPlaceBuyPanel.SetActive(show);
    }

    public void BuyVaksinPlace(int whichVaksinPlace)
    {
        if (vaksinPlacePoints[whichVaksinPlace].transform.childCount >= 1)//klo udh beli lgsg return
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
        GameObject go = Instantiate(vaksinPlaceObj, vaksinPlacePoints[whichVaksinPlace].transform.position, vaksinPlacePoints[whichVaksinPlace].transform.rotation) as GameObject;
        go.transform.parent = vaksinPlacePoints[whichVaksinPlace].transform;
        go.name = "vaksinPlace" + whichVaksinPlace;
        if (whichVaksinPlace == 0)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (whichVaksinPlace == 1)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (whichVaksinPlace == 2)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (whichVaksinPlace == 3)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        vaksinPlace.Add(go.GetComponent<VaksinPlace>());

        ShowBuyVaksinPlacePanel(false);
    }
}
