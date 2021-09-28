using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDataFeeder : MonoBehaviour
{
    public WindowGraph[] windowGraphs;
    [SerializeField] private float timeToFeedToGraph = 1f;

    private void Start()
    {
        //windowGraphs = GetComponentsInChildren<WindowGraph>();
        StartCoroutine(StartFeedingGraph());
    }
    
    private IEnumerator StartFeedingGraph()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToFeedToGraph);
            windowGraphs[0].AddGraphData(Citizen.instance.SickPeoples);
            windowGraphs[1].AddGraphData(Citizen.instance.HealthyPeoples + Citizen.instance.VaksinedPeoples + Citizen.instance.VaksinedPeoples2);
            windowGraphs[2].AddGraphData(Citizen.instance.HospitalizedPeoples);
        }
    }
}
