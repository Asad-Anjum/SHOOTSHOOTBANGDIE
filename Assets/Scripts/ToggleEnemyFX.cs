using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEnemyFX : MonoBehaviour
{
    // SAVE TOGGLES CODE


    [SerializeField] private Toggle[] toggles = new Toggle[7];

    private string[] toggle_names = { "SFX", "Music", "Camera Shake",
    "Die Effects", "Player Hit Effects", "Got Hurt Effect", "Glitch Effect"};
    //set int for player pref to 0 if toggle off, 1 if toggle on
    void Awake()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (PlayerPrefs.GetInt(toggle_names[i]) == 0)
            {
                toggles[i].isOn = false; //mistake: used .enabled instead of .isOn
            }
            else if (PlayerPrefs.GetInt(toggle_names[i]) == 1)
            {
                toggles[i].isOn = true;
            }
        }


    }

    // END OF SAVE TOGGLES CODE

    public void saveSelectedToggle(string name, int val)
    {
        PlayerPrefs.SetInt(name, val);
    }




    //[SerializeField] private GameObject toggles;
    //private SaveSelectedToggles saveTogs;
    [SerializeField] private GameObject cameraHolder;

    private void Update()
    {
        /*
        if (PublicVars.shakeCameraOn)
        {
            cameraHolder.GetComponent<ScreenShake>().enabled = true;
        }
        else
        {
            cameraHolder.GetComponent<ScreenShake>().enabled = false;
        } */
    }

    public void ToggleDieEffect(bool tog)
    {
        
        if (tog)
        {
            PublicVars.dieEffectsOn = true;
            saveSelectedToggle("Die Effects", 1);
            //saveTogs.saveSelectedToggle("Die Effects", 1);
        }
        else
        {
            PublicVars.dieEffectsOn = false;
            saveSelectedToggle("Die Effects", 0);
            //saveTogs.saveSelectedToggle("Die Effects", 0);
        }
    }

    public void TogglePlayerHitEffect(bool tog)
    {
        if (tog)
        {
            PublicVars.playerHitEffectsOn = true;
            saveSelectedToggle("Player Hit Effects", 1);
        }
        else
        {
            PublicVars.playerHitEffectsOn = false;
            saveSelectedToggle("Player Hit Effects", 0);
        }
    }

    public void ToggleGotHurt(bool tog)
    {
        if (tog)
        {
            PublicVars.gotHurtOn = true;
            saveSelectedToggle("Got Hurt Effect", 1);
        }
        else
        {
            PublicVars.gotHurtOn= false;
            saveSelectedToggle("Got Hurt Effect", 1);
        }
    }

    public void ToggleEnemyGlitch(bool tog)
    {
        if (tog)
        {
            PublicVars.glitchEffectOn = true;
            saveSelectedToggle("Glitch Effect", 1);
        }
        else
        {
            PublicVars.glitchEffectOn= false;
            saveSelectedToggle("Glitch Effect", 0);
        }
    }
    /*
    public void ToggleScreenShake(bool tog)
    {
        if (tog)
        {
            PublicVars.shakeCameraOn = true;
            //cameraHolder.GetComponent<ScreenShake>().enabled = true;
        }
        else
        {
            PublicVars.shakeCameraOn = false;
            //cameraHolder.GetComponent<ScreenShake>().enabled=false;
        }
    } */


    public void ToggleSFX(bool tog)
    {
        if (tog)
        {
            print("t");
            PublicVars.playSFX = true;
            saveSelectedToggle("SFX", 1);
        }
        else
        {
            print("f");
            PublicVars.playSFX = false;
            saveSelectedToggle("SFX", 0);
        }
    }




}
