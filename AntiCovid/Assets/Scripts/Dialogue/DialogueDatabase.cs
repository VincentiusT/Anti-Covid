using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Awake()
    {

        Beginning();

        dialogue.Add("RandomEventTutorial", new string[] {
            "Good Morning Sir!",
            "i heard that you're here to fight covid in this city",
            "that's a really great news",
            "well.. i hope all the best for you!",
            "i have some donation for you! not much but.. it may help you to build some facilities here!",
            "Good luck!!"
        });

        dialogue.Add("RandomEventForeign", new string[] {
            "Good Morning!",
            "I heard that you have a problem in handling covid here!",
            "well.. in that case, i have something for you!",
            "here is some vaccines to help you vaccinate all of your citizen!",
            "i hope that help!",
            "See you later!"
        });

        dialogue.Add("RandomEventOtomotif", new string[] {
            "Good Morning Sir!",
            "I really want you to finish this COVID problem",
            "a lot of people had a really hard time fighting this",
            "but i believe you can do this",
            "so these are some money for you!",
            "hope it will help you to go through this!",
            "Bye!"
        });

        dialogue.Add("RandomEventLembaga", new string[] {
            "Hello Sir!",
            "You are really a good person!",
            "You keep fighting and fighting again to beat this pandemic",
            "i really hope you can do that, so this is some money for you",
            "keep fighting and i hope you can beat this covid soon!",
            "See you later!"
        });

        dialogue.Add("RandomEventECommerce", new string[] {
            "Hello, excuse me!",
            "Are you fighting COVID in this city?",
            "wow, thats a really hard job!",
            "well in that case, i have something for you..",
            "here is some money to help you fight COVID in this city!",
            "Good luck and see you later!"
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

        dialogue.Add("semarang", new string[] {
            "My job is not over yet",
            "After Serang, i still have 8 cities left to save!",
            "Now it's time to save this city.. Semarang!",
            "Let's go!"
        });

        dialogue.Add("makassar", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Makassar!",
            "Let's go!"
        });

        dialogue.Add("bandung", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Bandung!",
            "Let's go!"
        });

        dialogue.Add("denpasar", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Denpasar!",
            "Let's go!"
        });

        dialogue.Add("pekanbaru", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Pekanbaru!",
            "Let's go!"
        });

        dialogue.Add("surabaya", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Surabaya!",
            "Let's go!"
        });

        dialogue.Add("jogja", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Jogjakarta!",
            "Let's go!"
        });

        dialogue.Add("jakarta", new string[] {
            "My job is not over yet",
            "Now it's time to save this city.. Jakarta!",
            "Let's go!"
        });
    }
}
