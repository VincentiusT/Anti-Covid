using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene("level" + level.ToString("0"));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
