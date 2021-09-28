using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject levelContainer;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject clickBlocker = null; //biar player ga pencet pas animasi
    private int maxLevelNow = 0;

    private void Start()
    {
        //PlayerPrefs.SetInt("MaxLevel", 0);
        //    if(PlayerPrefs.GetInt("AnimatedLevelUnlock") == 0)
        //    {
        //        PlayerPrefs.SetInt("AnimatedLevelUnlock", 1);
        //    }
        //    maxLevelNow = PlayerPrefs.GetInt("MaxLevel");
        //    if (maxLevelNow == 0)
        //        maxLevelNow = 1;
        maxLevelNow = PlayerPrefs.GetInt("MaxLevel");
        CheckForUnlockedLevel();
    }

    private void CheckForUnlockedLevel()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("AnimatedLevelUnlock") + 1; i++)
        {
            Transform levelPanel = levelContainer.transform.GetChild(i);
            levelPanel.GetChild(levelPanel.childCount - 2).gameObject.SetActive(false);
        }
        if(maxLevelNow > PlayerPrefs.GetInt("AnimatedLevelUnlock"))
        {
            AnimateLastUnlockedLevel();
            PlayerPrefs.SetInt("AnimatedLevelUnlock", maxLevelNow);
        }
    }

    private void AnimateLastUnlockedLevel()
    {
        //Debug.Log("ANIMASI WOI");
        Transform targetLevelPanel = levelContainer.transform.GetChild(maxLevelNow);
        Vector2 levelPositionInContainer = (Vector2)scrollRect.transform.InverseTransformPoint(levelContainer.transform.position) - (Vector2)scrollRect.transform.InverseTransformPoint(targetLevelPanel.transform.position);
        StartCoroutine(MoveLevelContainerToUnlockingLevel(levelPositionInContainer, targetLevelPanel));
    }

    private IEnumerator MoveLevelContainerToUnlockingLevel(Vector2 targetPos, Transform levelPanel)
    {
        Canvas.ForceUpdateCanvases();
        if (clickBlocker != null) clickBlocker.SetActive(true);

        Animator animator = levelPanel.GetChild(levelPanel.childCount - 2).gameObject.GetComponentInChildren<Animator>();
        animator.SetTrigger("Shake");

        yield return new WaitForSeconds(0.5f);
        float timeToScroll = 1f;
        while (timeToScroll > 0)
        {
            levelContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(levelContainer.GetComponent<RectTransform>().anchoredPosition, targetPos, 5f * Time.deltaTime);
            timeToScroll -= Time.deltaTime;
            yield return null;
        }

        //hilangin gembok

        animator.SetTrigger("Open");

        if (clickBlocker != null) clickBlocker.SetActive(false);
    }
}
