using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PharmacyManager.instance.pharmacyBuyPanel.activeSelf && !VaksinManager.instance.vaksinPlaceBuyPanel.activeSelf 
            && !HospitalManager.instance.hospitalBuyPanel.activeSelf && !OfficerManager.instance.officerBuyPanel.activeSelf && !Goverment.instance.govermentPanel.activeSelf
            && !GameManager.instance.pausePanel.activeSelf)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Hospital")
                {
                    HospitalManager.instance.ShowBuyHospitalPanel(true);
                }
                else if(hit.collider.tag == "Pharmacy")
                {
                    PharmacyManager.instance.ShowBuyPharmacyPanel(true);
                }
                else if (hit.collider.tag == "Goverment")
                {
                    Goverment.instance.ShowGovermentPanel(true);
                }
                else if (hit.collider.tag == "Officer")
                {
                    OfficerManager.instance.ShowBuyOfficerPanel(true);
                }
                else if (hit.collider.tag == "VaksinPlace")
                {
                    VaksinManager.instance.ShowBuyVaksinPlacePanel(true);
                }
            }
        }
    }
}
