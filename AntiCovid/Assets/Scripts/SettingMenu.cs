using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Toggle musicToggle, soundToggle;

    bool soundIsMuted, musicIsMuted;

    private void Start()
    {
        soundToggle.isOn = AudioManager.instance.muteAll == false ? true : false;
        //musicToggle.isOn = Inventory.musicMuted == false ? true : false;
    }

    public void MuteSound()
    {
        if (!soundIsMuted)
        {
            AudioManager.instance.muteAll = true;
        }
        else
        {
            AudioManager.instance.muteAll = false;
        }
        soundIsMuted = !soundIsMuted;
    }

    public void MuteMusic()
    {
        if (!musicIsMuted)
        {
            //Inventory.musicMuted = true;
            AudioManager.instance.Stop("music");
        }
        else
        {
            //Inventory.musicMuted = false;
            AudioManager.instance.Play("music");
        }


        musicIsMuted = !musicIsMuted;
    }
}
