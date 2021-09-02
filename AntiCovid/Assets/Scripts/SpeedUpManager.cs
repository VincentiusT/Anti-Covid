using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedUpManager : MonoBehaviour
{
    float[] availableMultiplier = { 1f, 2f, 4f };
    int indexMultiplier = 0;
    float timeMultiplier = 1f;

    public void SpeedUpButtonPress(TextMeshProUGUI buttonText)
    {
        indexMultiplier++;
        indexMultiplier %= availableMultiplier.Length;
        timeMultiplier = availableMultiplier[indexMultiplier];


        ChangeSpeedUpMultiplier();
        ChangeSpeedUpButtonText(buttonText);
    }

    private void ChangeSpeedUpButtonText(TextMeshProUGUI buttonText)
    {
        buttonText.text = ((int)timeMultiplier).ToString() + "x";
    }

    void ChangeSpeedUpMultiplier()
    {
        Debug.Log("change speedup time to: " + timeMultiplier);
        Time.timeScale = timeMultiplier;
    }
}
