using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBGMusic : MonoBehaviour
{
    private ToggleEnemyFX fx;
    public void TogglePlayTheme(bool tog)
    {
        if (tog)
        {
            PublicVars.playTheme = true;
            fx.saveSelectedToggle("Music", 1);
            print("PLAYING THEME");
        }
        else
        {
            PublicVars.playTheme = false;
            print("NOT PLAYING THEME");
            fx.saveSelectedToggle("Music", 0);
        }
    }
}
