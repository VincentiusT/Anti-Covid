using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBarInteraction : MonoBehaviour
{
    public static InfoBarInteraction instance;

    private Animator anim;
    private TextMeshProUGUI showDataText; 

    public bool isCurrentlyShowing;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        showDataText = transform.Find("infoBar/showMoreButton/Text (TMP)").GetComponent<TextMeshProUGUI>();
    }
    public void ShowMoreData(bool isShowing)
    {
        if (AudioManager.instance != null) AudioManager.instance.Play("tap");
        if (isCurrentlyShowing != isShowing)
        {
            anim.SetBool("up", false);
            isCurrentlyShowing = false;
            showDataText.text = "Show More..";
        }
        else
        {
            anim.SetBool("up", true);
            isCurrentlyShowing = true;
            showDataText.text = "Show Less..";
        }
    }

}
