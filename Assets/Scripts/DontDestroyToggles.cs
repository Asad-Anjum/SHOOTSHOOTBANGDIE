using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyToggles : MonoBehaviour
{
    [SerializeField] Toggle[] toggles = new Toggle[7];
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            DontDestroyOnLoad(toggles[i]);
        }
    }

    
    
}
