using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int randomEventChance;
    [SerializeField] private List<RandomEvent> randomEvents;
    [SerializeField] private DialogueSystem dialogueSystem;

    private void Start()
    {
        DayManager.onDayChangeCallback += RollRandomEvent;
    }

    private void RollRandomEvent(int day)
    {
        Debug.Log("Rolling random event");
        if (isRandomEventHappening())
        {
            CreateRandomEvent();
        }
    }

    private void CreateRandomEvent()
    {
        int randomEventIndex = UnityEngine.Random.Range(0, randomEvents.Count);
        RandomEvent selectedRandomEvent = randomEvents[randomEventIndex];

        RunRandomEvent(selectedRandomEvent);
    }

    private void RunRandomEvent(RandomEvent selectedRandomEvent)
    {
        Debug.Log("Running random event: " + selectedRandomEvent.eventName);

        switch (selectedRandomEvent.randomEventType)
        {
            case RandomEventType.MONEY:
                Goverment.instance.Money += selectedRandomEvent.ammount;
                break;
            case RandomEventType.VACCINE:
                VaksinManager.instance.VaccineStock += selectedRandomEvent.ammount;
                break;
            default:
                Debug.Log("Random Event Type is Missing");
                break;
        }

        dialogueSystem.PlayNewDialogue(selectedRandomEvent.dialogueID, selectedRandomEvent.donor.donorSprite);
    }

    private bool isRandomEventHappening()
    {
        return randomEventChance > UnityEngine.Random.Range(0, 100);
    }
}

[Serializable]
public class RandomEvent
{
    public int id;
    public string eventName;
    public RandomEventType randomEventType;
    public int ammount;
    public string dialogueID;
    public Donor donor;
}

public enum RandomEventType
{
    VACCINE, MONEY
}

[Serializable]
public class Donor
{
    public string donorName;
    public Sprite donorSprite;
    public string stories;
}