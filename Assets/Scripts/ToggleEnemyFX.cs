using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEnemyFX : MonoBehaviour
{
    public void ToggleDieEffect(bool tog)
    {
        
        if (tog)
        {
            PublicVars.dieEffectsOn = true;

        }
        else
        {
            PublicVars.dieEffectsOn = false;
        }
    }

    public void TogglePlayerHitEffect(bool tog)
    {
        if (tog)
        {
            PublicVars.playerHitEffectsOn = true;
        }
        else
        {
            PublicVars.playerHitEffectsOn = false;
        }
    }
}
