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
        collectableHouseNotification.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(timeElapsed > cooldownTimeToGenerateMoney)
        {
            if (!isReadyToBeCollected)
            {
                isReadyToBeCollected = true;
                collectableHouseNotification.gameObject.SetActive(true);
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
            isReadyToBeCollected = false;
            timeElapsed = 0f;

            if (collectableHouseNotification.gameObject.activeSelf)
            {
                collectableHouseNotification.gameObject.SetActive(false);
            }
        }
    }

    private int RandomizeMoneyGet()
    {
        return Random.Range(lowerLimitMoney, upperLimitMoney);
    }
}
