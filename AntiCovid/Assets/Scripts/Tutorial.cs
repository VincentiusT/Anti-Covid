using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField] private GameObject canvasTutorial, canvasGeneralTutorial;

    private bool isFinished = true;

    public bool IsFinished
    {
        get { return isFinished; }
    }

    private void Start()
    {
        instance = this;
        //PlayerPrefs.SetInt("crowdTutorial", 0);
        //PlayerPrefs.SetInt("generalTutorial", 0);
        //StartTutorial();
    }

    public void StartTutorial()
    {
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
}
