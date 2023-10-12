using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : MonoBehaviour
{
    private ScreenShake ss;
    public float speed;
    private Transform playerPos;
    private Player player;

    public GameObject[] playerHitEffect;
    public GameObject[] dieEffect;
    public int x = 0;


    

    void Start()
    {
        ss = GameObject.FindGameObjectWithTag("Shake").GetComponent<ScreenShake>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {


        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Instantiate(playerHitEffect[x], transform.position, Quaternion.identity); //Toggle this on/off
            Vector3 oldPos = ss.positionStrength;
            Vector3 oldRot = ss.rotationStrength;
            ss.positionStrength = new Vector3(0.6f,0.6f,0f);
            ss.rotationStrength = new Vector3(0.6f,0.6f,0f);
            ScreenShake.Invoke();//Toggle this on/off
            ss.positionStrength = oldPos;
            ss.rotationStrength = oldRot;
            Camera.main.GetComponent<GlitchEffect>().enabled = true;
            player.health--;
            Destroy(gameObject);
        }

        if(col.tag == "Bullet")
        {
            Instantiate(dieEffect[x], transform.position, Quaternion.identity); //Toggle this on/off
            ScreenShake.Invoke();//Toggle this on/off
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
