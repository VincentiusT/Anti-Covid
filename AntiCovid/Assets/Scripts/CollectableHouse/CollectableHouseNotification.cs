using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHouseNotification : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //play animator
        //Debug.Log("Hai");
    }
}
