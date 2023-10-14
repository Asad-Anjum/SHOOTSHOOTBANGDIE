using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Transform playerPos;
    Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBtwFiring;

    private ScreenShake ss;

    //bool playSound;

    void Start()
    {
        playerPos = GetComponent<Transform>();
        ss = GameObject.FindGameObjectWithTag("Shake").GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ -90);


        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBtwFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            if (PublicVars.playSFX) //(playSound)
            {
                FindObjectOfType<MusicManager>().PlaySoundEffects("Laser");
            }
            
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);

            Vector3 oldPos = ss.positionStrength;
            Vector3 oldRot = ss.rotationStrength;
            ss.positionStrength = new Vector3(0.1f,0.1f,0f);
            ss.rotationStrength = new Vector3(0.1f,0.1f,0f);
            ScreenShake.Invoke();//Toggle this on/off
            ss.positionStrength = oldPos;
            ss.rotationStrength = oldRot;
        }
    }
    /*
    public void ToggleSFX(bool tog)
    {
        if(tog)
        {
            playSound = true;
        }
        else {
            playSound=false;
        }
    } */
}
