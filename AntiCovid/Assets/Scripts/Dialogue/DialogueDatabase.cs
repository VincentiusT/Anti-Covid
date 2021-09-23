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
            "So this is Serang..",
            "i want to know about this city more, but for now i need to save this city first",
            "i need to build facilities here to help this city fight covid-19",
            "So let's begin this!"
        });

        dialogue.Add("semarang", new string[] {
            "Well, well, finally i arrived in Semarang",
            "i heard that there is a lot of great food here..",
            "but my job is not over yet",
            "After Serang, i still have 8 cities left to save!",
            "Now it's time to save this city.. Semarang!",
            "Let's go!"
        }) ;

        dialogue.Add("makassar", new string[] {
            "Helloooo, Makassar..",
            "After a long flight finally i arrived here",
            "no time to play, i need to save this city now",
            "i hope i can make peoples in this city more aware and vaccinate all! Let's go!"
        });

        dialogue.Add("bandung", new string[] {
            "Bandung! i always want to be here!",
            "nice weather, lot of foods, and many more!",
            "but now it's time to save this city..",
            "Let's fight covid-19 here!"
        });

        dialogue.Add("denpasar", new string[] {
            "After Bandung, now it's Denpasar..",
            "i know i can do this! i will never give up saving all of the cities",
            "and now it's time for Denpasar to be saved",
            "Let's do this!"
        });

        dialogue.Add("pekanbaru", new string[] {
            "Finally i arrived in here, Pekanbaru",
            "it's the same here! build facilites, policies, and fight covid-19!",
            "New place, new victory!",
            "Let's go!"
        });

        dialogue.Add("surabaya", new string[] {
            "Woww, so this is what they call City of Heroes, Surabaya!",
            "It is a really nice and great city!",
            "But the problems remains the same.. covid-19",
            "Let's go fight covid-19 and save this city!",
            "Let's do this!"
        });

        dialogue.Add("jogja", new string[] {
            "Now.. it's time to save Jogjakarta!",
            "i really love this city",
            "But right now, after covid-19 attacks, there are a lot of problems that need to be fixed",
            "So, i am gonna build some facilities and make policies to save this city!",
            "Let's go!"
        });

        dialogue.Add("jakarta", new string[] {
            "Finally, i arrived at the capital city of Indonesia, Jakarta!!",
            "There are a lot of peoples here, 10 Million peoples!",
            "And the covid-19 transmission rate is really high in here!",
            "So i need to prepare and fight covid-19 in this city!",
            "i hope i can save this city and vaccinate a lot of people here",
            "Let's go!"
        });
    }
}
