using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHouse : MonoBehaviour
{
    [SerializeField] private int lowerLimitMoney, upperLimitMoney;
    [SerializeField] private float cooldownTimeToGenerateMoney;

    public float timeElapsed;
    private bool isReadyToBeCollected;

    public CollectableHouseNotification collectableHouseNotification;

    private void Start()
    {
        collectableHouseNotification = GetComponentInChildren<CollectableHouseNotification>();
        SetHouseNotification(false);
    }

    private void Update()
    {
        if(timeElapsed > cooldownTimeToGenerateMoney)
        {
            if (!isReadyToBeCollected)
            {
                isReadyToBeCollected = true;
                SetHouseNotification(true);
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        CollectMoney();
    }

    private void CollectMoney()
    {
        if (isReadyToBeCollected)
        {
            Goverment.instance.Money += RandomizeMoneyGet();
            ResetHouseAttribute();
            SetHouseNotification(false);
        }
    }

    private void ResetHouseAttribute()
    {
        isReadyToBeCollected = false;
        timeElapsed = 0f;
    }

    private void SetHouseNotification(bool isActive)
    {
        if (collectableHouseNotification.gameObject.activeSelf != isActive)
        {
            collectableHouseNotification.gameObject.SetActive(isActive);
        }
    }

    private int RandomizeMoneyGet()
    {
        return Random.Range(lowerLimitMoney, upperLimitMoney);
    }
}
