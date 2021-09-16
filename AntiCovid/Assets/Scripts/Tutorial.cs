using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public bool tutorialLevel = false;
    public GameObject canvasTutorial, canvasGeneralTutorial, objectTutorial;
    private bool isBuyTutorial = true;
    private bool isFinished = true;
    private bool lastTutorial;

    private GameObject tutorialHospitalBuyPanel, tutorialVaccinationBuyPanel, governmentPanel, tutorialLastPanel, tutorialHospitalizedPanel;

    public bool IsBuyTutorial
    {
        set { isBuyTutorial = value; }
        get { return isBuyTutorial; }
    }

    public bool LastTutorial
    {
        get { return lastTutorial; }
    }
    public void TurnOffLastTutorial()
    {
        lastTutorial = false;
    }

    public bool IsFinished
    {
        get { return isFinished; }
    }

    public GameObject TutorialHospitalize
    {
        get { return tutorialHospitalizedPanel; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!tutorialLevel) isBuyTutorial = false;

        tutorialHospitalBuyPanel = canvasGeneralTutorial.transform.Find("Tutorial Hospital Buy").gameObject;
        tutorialVaccinationBuyPanel = canvasGeneralTutorial.transform.Find("Tutorial Vaccination Buy").gameObject;
        governmentPanel = canvasGeneralTutorial.transform.Find("Tutorial Government").gameObject;
        tutorialLastPanel = canvasGeneralTutorial.transform.Find("Tutorial Last").gameObject;
        tutorialHospitalizedPanel = canvasGeneralTutorial.transform.Find("Tutorial Hospitalize").gameObject;

        //PlayerPrefs.SetInt("crowdTutorial", 0);
        PlayerPrefs.SetInt("generalTutorial", 0);
        //StartTutorial();
    }

    public void StartTutorial()
    {
        if (!tutorialLevel) return;

        if (PlayerPrefs.GetInt("generalTutorial") == 1)
        {
            return;
        }
        else
        {
            isFinished = false;
            canvasGeneralTutorial.SetActive(true);
            PlayerPrefs.SetInt("generalTutorial", 1);
            Time.timeScale = 0f;
        }
    }

    public void resumeAfterTutorial()
    {
        HospitalTutorial();
        isFinished = true;
        Time.timeScale = 1f;
    }

    public void ShowCrowdTutorial(Transform crowdPosition)
    {
        if (PlayerPrefs.GetInt("crowdTutorial") == 1) return;

        PlayerPrefs.SetInt("crowdTutorial", 1);
        canvasTutorial.SetActive(true);
        GameObject tutorialPanelGO = canvasTutorial.transform.Find("Tutorial Crowd").gameObject;
        tutorialPanelGO.SetActive(true);
        tutorialPanelGO.transform.position = crowdPosition.localPosition;
        //CrowdTutorialPanel.SetActive(true);
    }

    public void HospitalTutorial()
    {
        //yield return new WaitForSeconds(2f);
        objectTutorial.SetActive(true);
    }

    public IEnumerator StopHospitalTutorial()
    {
        tutorialHospitalBuyPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        VaccinationPlaceTutorial();
    }

    public void VaccinationPlaceTutorial()
    {
        isBuyTutorial = true;
        objectTutorial.transform.Find("Tutorial VaccinationPlace").gameObject.SetActive(true);
    }

    public IEnumerator StopVaccinationTutorial()
    {
        tutorialVaccinationBuyPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        GovernmentTutorial();
    }

    public void GovernmentTutorial()
    {
        isBuyTutorial = true;
        objectTutorial.transform.Find("Tutorial Government").gameObject.SetActive(true);
    }

    public IEnumerator StopGovernmentTutorial()
    {
        governmentPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        TutorialLast();
    }

    public void TutorialLast()
    {
        lastTutorial = true;
        tutorialLastPanel.SetActive(true);
    }
}
