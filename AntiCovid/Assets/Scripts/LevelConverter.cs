using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelConverter : MonoBehaviour
{
    public static int ConvertLevelToIndex()
    {
        return SceneManager.GetActiveScene().buildIndex-2;
        
    }
}
