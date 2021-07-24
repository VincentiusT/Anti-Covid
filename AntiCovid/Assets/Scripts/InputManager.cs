using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    int UILayer;

    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement()/* !PharmacyManager.instance.pharmacyBuyPanel.activeSelf && !VaksinManager.instance.vaksinPlaceBuyPanel.activeSelf 
            && !HospitalManager.instance.hospitalBuyPanel.activeSelf && !OfficerManager.instance.officerBuyPanel.activeSelf && !Goverment.instance.govermentPanel.activeSelf
            && !GameManager.instance.pausePanel.activeSelf*/)
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
                    //if(!InfoBarInteraction.instance.isCurrentlyShowing)
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

    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
