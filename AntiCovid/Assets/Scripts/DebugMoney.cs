using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMoney : MonoBehaviour
{
    public void IncreaseDebugMoney()
    {
        Goverment.instance.Money += 100000;
    }
}
