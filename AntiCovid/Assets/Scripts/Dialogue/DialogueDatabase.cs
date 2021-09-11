using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Awake()
    {

        Beginning();
        

        dialogue.Add("RandomEventForeign", new string[] {
            "Good Morning!",
            "I heard that you have a problem in handling covid here!",
            "well.. in that case, i have something for you!",
            "here is some vaccines to help you vaccinate all of your citizen!",
            "i hope that help!",
            "See you later!"
        });

        dialogue.Add("RandomEventOtomotif", new string[] {
            "Good Morning!",
            "I heard that you have a problem in handling covid here!",
            "well.. in that case, i have something for you!",
            "here is some vaccines to help you vaccinate all of your citizen!",
            "i hope that help!",
            "See you later!"
        });

        dialogue.Add("RandomEventLembaga", new string[] {
            "Good Morning!",
            "I heard that you have a problem in handling covid here!",
            "well.. in that case, i have something for you!",
            "here is some vaccines to help you vaccinate all of your citizen!",
            "i hope that help!",
            "See you later!"
        });

        dialogue.Add("RandomEventECommerce", new string[] {
            "Good Morning!",
            "I heard that you have a problem in handling covid here!",
            "well.. in that case, i have something for you!",
            "here is some vaccines to help you vaccinate all of your citizen!",
            "i hope that help!",
            "See you later!"
        });


    }

    void Beginning()
    {
        dialogue.Add("test", new string[] {
            "Finally i arrived in this city!",
            "But this is only the start, i need to save this city",
            "i need to make people aware of this pandemic, make a policy, build a lot of facilities, and many more",
            "well, i will do my best! Let's do this!"
        });

        dialogue.Add("serang", new string[] {
            "Woaahh..",
            "Finally i arrived in this city.. Serang!",
            "But this is only the start, i need to save this city",
            "i need to make people aware of this pandemic, make a policy, build a lot of facilities, and many more",
            "well, i will do my best! Let's do this!"
        });
    }
}
