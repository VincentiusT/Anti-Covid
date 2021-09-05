using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Door
        dialogue.Add("Beginning", new string[] {
            "Finally i arrived in this city.. Serang!",
            "But this is the start, i need to save this city",
            "i need to make people aware of this pandemic, make a policy, build a lot of facilities, and many more",
            "Let's do this!"
        });

        dialogue.Add("RandomEventMoney", new string[] {
            "hehe boi",
            "hehe boi boi"
        });

    }
}
