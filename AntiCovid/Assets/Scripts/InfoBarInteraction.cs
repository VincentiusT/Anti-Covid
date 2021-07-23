using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBarInteraction : MonoBehaviour
{
    public static InfoBarInteraction instance;

    private Animator anim;

    public bool isCurrentlyShowing;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ShowMoreData(bool isShowing)
    {
        if (isCurrentlyShowing != isShowing)
        {
            anim.SetBool("up", false);
            isCurrentlyShowing = false;
        }
        else
        {
            anim.SetBool("up", true);
            isCurrentlyShowing = true;
        }
    }

}
