using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField] private GameObject canvasTutorial;

    private void Start()
    {
        instance = this;
        
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
