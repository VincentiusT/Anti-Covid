using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winPanel;
    public GameObject losePanel;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f ;
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
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene("menu");
    }
}
