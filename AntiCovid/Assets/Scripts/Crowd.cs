using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public bool inMall;
    private Animator anim;
    [SerializeField] private GameObject tandaSeru;
    private List<Animator> peopleAnims;

    private int transmissionIncreaseRate = 10;

    float timeToIncreaseTransmissionRate = 10f;
    float temp;
    private void Start()
    {
        temp = timeToIncreaseTransmissionRate;

        if (!inMall)
        {
            anim = GetComponent<Animator>();

            peopleAnims = new List<Animator>();
            for(int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                peopleAnims.Add(transform.GetChild(0).GetChild(i).GetComponent<Animator>());
            }
        }
        else
        {
            StopCrowdWalking();
        }
    }

    public void StopCrowdWalking()
    {
        if (!inMall)
        {
            for (int i = 0; i < peopleAnims.Count; i++)
            {
                peopleAnims[i].SetTrigger("stop");
            }
        }
        GameObject go = Instantiate(tandaSeru, transform.position, transform.rotation);
        go.transform.position += new Vector3(0, 0.5f, 0);
        go.transform.SetParent(transform);

        //BUAT TUTORIAL
        Tutorial.instance.ShowCrowdTutorial(transform);
    }

    public void DestroyCrowd()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Crowd" && hit.collider.gameObject.transform.parent.name==gameObject.name)
                {
                    DestroyCrowd();
                }
            }
        }

        if (timeToIncreaseTransmissionRate <= 0)
        {
            
            IncreaseTransmissionRate();
            timeToIncreaseTransmissionRate = temp;
        }
        else
        {
            timeToIncreaseTransmissionRate -= Time.deltaTime;
        }
    }

    private void IncreaseTransmissionRate()
    {
        Debug.Log("test : +" + transmissionIncreaseRate);
        Citizen.instance.TransmissionRateTotal += transmissionIncreaseRate;
    }
}
