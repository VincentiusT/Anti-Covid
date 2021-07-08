using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject difficulty;

    

    public void Play(int dif)
    {
        PlayerPrefs.SetInt("diff", dif);
        SceneManager.LoadScene("main");
    }

    public void showDifficultyPanel(bool isShowing)
    {
        difficulty.SetActive(isShowing);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
