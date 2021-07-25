using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private NeighbourWaypointWrapper neighbourWaypointOne = null, neighbourWaypointTwo = null;

    public NeighbourWaypointWrapper GetRandomNeighbour()
    {
        if (neighbourWaypointOne.waypoint == null)
            return neighbourWaypointTwo;
        else if (neighbourWaypointTwo.waypoint == null)
        {
            Debug.Log("Null");
            return neighbourWaypointOne;
        }

        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
            return neighbourWaypointOne;
        else
            return neighbourWaypointTwo;
    }
}

[System.Serializable]
public class NeighbourWaypointWrapper
{
    public Waypoint waypoint;
    public ArahJalan arahJalan;
    public int orderLayer;

    public NeighbourWaypointWrapper(Waypoint _waypoint, ArahJalan _arahJalan, int _orderLayer)
    {
        waypoint = _waypoint;
        arahJalan = _arahJalan;
        orderLayer = _orderLayer;
    }
}

public enum ArahJalan{
    KIRI_ATAS, KANAN_ATAS, KIRI_BAWAH, KANAN_BAWAH
}