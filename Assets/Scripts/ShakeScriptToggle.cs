using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScriptToggle : MonoBehaviour
{

    //public Behaviour shake;
    bool screenShake;

    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<ScreenShake>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!screenShake)
        {
            this.GetComponent<ScreenShake>().enabled = false;
        }
        else
        {
            this.GetComponent<ScreenShake>().enabled = true;
        }
    }

    public void toggleScreenShake(bool tog)
    {
        if (tog)
        {
            screenShake = true;
        }
        else
        {
            screenShake = false;
        }
    }
}
