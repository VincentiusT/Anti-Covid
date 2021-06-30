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
        if (Input.GetMouseButtonDown(0))
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

                }
                else if (hit.collider.tag == "Goverment")
                {

                }
                else if (hit.collider.tag == "Officer")
                {

                }
                else if (hit.collider.tag == "VaksinPlace")
                {

                }
            }
        }
    }
}
