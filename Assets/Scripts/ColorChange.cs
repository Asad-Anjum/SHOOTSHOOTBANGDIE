using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material[] mat;
    public int x;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = true;
        rend.sharedMaterial = mat[x];
    }

    public void MatChange()
    {
        if(x == 0) x = 1;
        else if(x == 1) x = 0;

        rend.sharedMaterial = mat[x];

        if(this.gameObject.tag == "Enemy")
        {
            if (this.GetComponent<Enemy>().x == 0)
                this.GetComponent<Enemy>().x = 1;
            if(this.GetComponent<Enemy>().x == 1)
                this.GetComponent<Enemy>().x = 0;
        }
    }

    public void MatFreeze()
    {
        rend.sharedMaterial = mat[2];
        x = 2;
        this.GetComponent<Enemy>().x = 2;
    }
}
