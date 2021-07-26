using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    float minTimeToOpenMenu = 0.2f, tapTimer;
    string hitPlace;

    int UILayer;

    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && IsPointerOverUIElement())
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            tapTimer -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement()/* !PharmacyManager.instance.pharmacyBuyPanel.activeSelf && !VaksinManager.instance.vaksinPlaceBuyPanel.activeSelf 
            && !HospitalManager.instance.hospitalBuyPanel.activeSelf && !OfficerManager.instance.officerBuyPanel.activeSelf && !Goverment.instance.govermentPanel.activeSelf
            && !GameManager.instance.pausePanel.activeSelf*/)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                tapTimer = minTimeToOpenMenu;
                hitPlace = hit.collider.tag;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (tapTimer > 0f)
            {
                Debug.Log("WOI");
                if (hitPlace == "Hospital")
                {
                    HospitalManager.instance.ShowBuyHospitalPanel(true);
                }
                else if (hitPlace == "Pharmacy")
                {
                    PharmacyManager.instance.ShowBuyPharmacyPanel(true);
                }
                else if (hitPlace == "Goverment")
                {
                    //if(!InfoBarInteraction.instance.isCurrentlyShowing)
                    Goverment.instance.ShowGovermentPanel(true);
                }
                else if (hitPlace == "Officer")
                {
                    OfficerManager.instance.ShowBuyOfficerPanel(true);
                }
                else if (hitPlace == "VaksinPlace")
                {
                    VaksinManager.instance.ShowBuyVaksinPlacePanel(true);
                }
                hitPlace = null;
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
