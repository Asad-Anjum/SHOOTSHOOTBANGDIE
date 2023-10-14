using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSelectedToggles : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles = new Toggle[7];

    private string[] toggle_names = { "SFX", "Music", "Camera Shake", 
    "Die Effects", "Player Hit Effects", "Got Hurt Effect", "Glitch Efect"};
    //set int for player pref to 0 if toggle off, 1 if toggle on
    void Awake()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (PlayerPrefs.GetInt(toggle_names[i]) == 0)
            {
                toggles[i].enabled = false;
            }
            else if (PlayerPrefs.GetInt(toggle_names[i]) == 1)
            {
                toggles[i].enabled = true;
            }
        }


    }

    public void saveSelectedToggle(string name, int val)
    {
        PlayerPrefs.SetInt(name, val);
    }
}
