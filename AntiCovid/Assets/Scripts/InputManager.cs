using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private Camera mainCamera;
    Vector2 firstMousePosition, lastMousePosition;
    string hitPlace;

    int UILayer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Tutorial.instance !=null && !Tutorial.instance.IsFinished) return;
        if(Input.GetMouseButtonDown(0) && IsPointerOverUIElement())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement() /* !PharmacyManager.instance.pharmacyBuyPanel.activeSelf && !VaksinManager.instance.vaksinPlaceBuyPanel.activeSelf 
            && !HospitalManager.instance.hospitalBuyPanel.activeSelf && !OfficerManager.instance.officerBuyPanel.activeSelf && !Goverment.instance.govermentPanel.activeSelf
            && !GameManager.instance.pausePanel.activeSelf*/)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                firstMousePosition = mainCamera.WorldToScreenPoint(Input.mousePosition);
                hitPlace = hit.collider.tag;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //if (tapTimer > 0f)
            if(Vector2.Distance(firstMousePosition, mainCamera.WorldToScreenPoint(Input.mousePosition)) < 0.5f)
            {
                if (AudioManager.instance != null) AudioManager.instance.Play("tap");

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
                else if(hitPlace == "Monas")
                {
                    UIManager.instance.ShowNotifPanel("This is Jakarta's National Monumment, Monas!");
                }
                else if (hitPlace == "Bandung")
                {
                    UIManager.instance.ShowNotifPanel("This is Bandung's Landmark, Gedung Sate!");
                }
                else if (hitPlace == "Semarang")
                {
                    UIManager.instance.ShowNotifPanel("This is Semarang's Landmark, Lawang Sewu!");
                }
                else if (hitPlace == "Surabaya")
                {
                    UIManager.instance.ShowNotifPanel("This is Surabaya's Monumment, Sura and Baya!");
                }
                else if (hitPlace == "Pekanbaru")
                {
                    UIManager.instance.ShowNotifPanel("This is Pekanbaru's Landmark, Candi Muara Takus!");
                }
                else if (hitPlace == "Jogja")
                {
                    UIManager.instance.ShowNotifPanel("This is Jogjakarta's Monumment, Tugu Jogja Monumment!");
                }
                else if (hitPlace == "Denpasar")
                {
                    UIManager.instance.ShowNotifPanel("This is Denpasar's Landmark!");
                }
                else if (hitPlace == "Serang")
                {
                    UIManager.instance.ShowNotifPanel("This is Serang's Monumment, Masjid Agung Banten!");
                }
                else if (hitPlace == "Makassar")
                {
                    UIManager.instance.ShowNotifPanel("This is Makassar's Landmark, Masjid 99 kubah!");
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
