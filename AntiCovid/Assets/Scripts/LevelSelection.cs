using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    public void GoToLevel(int level)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        SceneManager.LoadScene("level" + level.ToString("0"));
        //SceneManager.LoadScene("main");
    }

    public void BackToMenu()
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        SceneManager.LoadScene("menu");
    }
}
