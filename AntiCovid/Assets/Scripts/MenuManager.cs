using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject SettingPanel;

    public void Play()
    {
        SceneManager.LoadScene("level");
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
