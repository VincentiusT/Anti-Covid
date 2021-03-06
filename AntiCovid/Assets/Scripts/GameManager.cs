using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;

    bool done;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }

    public void Win()
    {
        if(PlayerPrefs.GetInt("MaxLevel") <= LevelConverter.ConvertLevelToIndex())
            PlayerPrefs.SetInt("MaxLevel", PlayerPrefs.GetInt("MaxLevel") + 1);
        if (!done)
        {
            if (AudioManager.instance != null) AudioManager.instance.Play("win");
            done = true;
        }
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        
        Inventory.instance.Save(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Home()
    {
        Inventory.instance.Save(Citizen.instance.VaksinedPeoples2 >= Citizen.instance.TotalCitizen * 0.75);
        SceneManager.LoadScene("menu");
    }

    public void Pause(bool pause)
    {
        if (pause == true) Time.timeScale = 0f;
        else if (pause == false) Time.timeScale = 1f;

        pausePanel.SetActive(pause);
    }


}
