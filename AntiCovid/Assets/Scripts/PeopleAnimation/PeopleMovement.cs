using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovement : MonoBehaviour
{
    [SerializeField] private GameObject waypointContainer;
    private Queue<NeighbourWaypointWrapper> nextWaypointVisited;
    [SerializeField] private float moveSpeed = 50f;
    private Waypoint lastWaypoint = null;
    [SerializeField] ArahJalan firstWaypointArahJalan;
    [SerializeField] private float timeStop = 3f;
    [SerializeField][Range(0, 100)] private int chanceToImmidiatelyMove = 10;

    private void Start()
    {
        nextWaypointVisited = new Queue<NeighbourWaypointWrapper>();

        NeighbourWaypointWrapper firstWaypoint = new NeighbourWaypointWrapper(waypointContainer.transform.GetChild(0).GetComponent<Waypoint>(), firstWaypointArahJalan, 10);
        nextWaypointVisited.Enqueue(firstWaypoint);
        SearchForNextWaypoint();
    }

    private void SearchForNextWaypoint()
    {
        if (HaveNextWaypoint())
        {
            NeighbourWaypointWrapper nextWaypointWrapper = nextWaypointVisited.Dequeue();

            Debug.Log("Heading to " + nextWaypointWrapper.waypoint.transform.name);

            StartCoroutine(StartMoving(nextWaypointWrapper.waypoint, nextWaypointWrapper.arahJalan, nextWaypointWrapper.orderLayer));

            lastWaypoint = nextWaypointWrapper.waypoint;
        }
        else
        {
            EnqueueNextWaypoint();
        }
    }

    private IEnumerator StartMoving(Waypoint waypoint, ArahJalan arahJalan, int orderLayer)
    {
        //TODO: set animator to coresponding animation
        PlayAnimation(arahJalan);
        GetComponent<SpriteRenderer>().sortingOrder = orderLayer;


        while (true)
        {
            //move towards waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, moveSpeed * Time.deltaTime);
            //kalau sudah sampai, break
            Debug.Log(Vector3.Distance(transform.localPosition, waypoint.transform.position));
            if(Vector3.Distance(transform.position, waypoint.transform.position) < 0.01f)
            {
                break;
            }
            yield return null;
        }
        int moveImmediatelyChance = UnityEngine.Random.Range(0, 100);
        if (moveImmediatelyChance < chanceToImmidiatelyMove)
            yield return null;
        else
            yield return new WaitForSeconds(3f);

        SearchForNextWaypoint();
    }

    private void PlayAnimation(ArahJalan arahJalan)
    {
        Animator animator = GetComponent<Animator>();
        switch (arahJalan)
        {
            case ArahJalan.KANAN_ATAS:
                animator.Play("KananAtas");
                break;
            case ArahJalan.KANAN_BAWAH:
                animator.Play("KananBawah");
                break;
            case ArahJalan.KIRI_ATAS:
                animator.Play("KiriAtas");
                break;
            case ArahJalan.KIRI_BAWAH:
                animator.Play("KiriBawah");
                break;
        }
    }

    private bool HaveNextWaypoint()
    {
        return nextWaypointVisited.Count != 0;
    }

    private void EnqueueNextWaypoint()
    {
        NeighbourWaypointWrapper randomWaypoint = lastWaypoint.GetRandomNeighbour();
        nextWaypointVisited.Enqueue(randomWaypoint);
        SearchForNextWaypoint();
    }
}
