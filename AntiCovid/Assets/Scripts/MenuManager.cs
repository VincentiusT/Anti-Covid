using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject SettingPanel;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        AudioManager.instance.Play("tap");
        SceneManager.LoadScene("level");
    }

    public void showSetting(bool isShowing)
    {
        AudioManager.instance.Play("tap");
        SettingPanel.SetActive(isShowing);
    }

    public void Quit()
    {
        AudioManager.instance.Play("tap");
        Application.Quit();
    }
}
