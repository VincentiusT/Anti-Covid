using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject difficulty;
    public GameObject SettingPanel;

    public void Play(int dif)
    {
        PlayerPrefs.SetInt("diff", dif);
        SceneManager.LoadScene("main");
    }

    public void showDifficultyPanel(bool isShowing)
    {
        difficulty.SetActive(isShowing);
    }

    public void showSetting(bool isShowing)
    {
        SettingPanel.SetActive(isShowing);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
