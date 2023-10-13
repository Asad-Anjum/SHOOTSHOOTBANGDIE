using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBGMusic : MonoBehaviour
{
    public void TogglePlayTheme(bool tog)
    {
        if (tog)
        {
            PublicVars.playTheme = true;
        }
        else
        {
            PublicVars.playTheme = false;
        }
    }
}
