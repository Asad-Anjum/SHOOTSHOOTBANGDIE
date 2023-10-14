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

    bool playSFX;

    private Material mat;

    private Rigidbody2D rb;
    public float thrust;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ss = GameObject.FindGameObjectWithTag("Shake").GetComponent<ScreenShake>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        mat = this.GetComponent<SpriteRenderer>().material;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(PushBack());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            x = 3;
            Vector3 oldPos = ss.positionStrength;
            Vector3 oldRot = ss.rotationStrength;
            ss.positionStrength = new Vector3(5f,5f,0f);
            ss.rotationStrength = new Vector3(5f,5f,0f);
            EnemyDeath();
            ss.positionStrength = oldPos;
            ss.rotationStrength = oldRot;
            
        }

        if (playSFX)
        {
            print("play");
        }

        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    private IEnumerator PushBack()
    {
        if(x==2) EnemyDeath();
        float oldSpeed = speed;
        speed = 0;
        rb.AddForce((transform.position - playerPos.position) * thrust);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = new Vector2(0f,0f);
        speed = oldSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (PublicVars.playerHitEffectsOn)
            {
                Instantiate(playerHitEffect[x], transform.position, Quaternion.identity);
            }
            //Instantiate(playerHitEffect[x], transform.position, Quaternion.identity); //Toggle this on/off
            Vector3 oldPos = ss.positionStrength;
            Vector3 oldRot = ss.rotationStrength;
            ss.positionStrength = new Vector3(1f,1f,0f);
            ss.rotationStrength = new Vector3(1f,1f,0f);
            ScreenShake.Invoke();//Toggle this on/off
            ss.positionStrength = oldPos;
            ss.rotationStrength = oldRot;
            Camera.main.GetComponent<GlitchEffect>().enabled = true;
            player.health--;
            Destroy(gameObject);
        }

        if(col.tag == "Bullet")
        {
            EnemyDeath();
            Destroy(col.gameObject);
        }
    }

    void EnemyDeath()
    {
            if (PublicVars.dieEffectsOn)
            {
                Instantiate(dieEffect[x], transform.position, Quaternion.identity);
            }
            ScreenShake.Invoke();//Toggle this on/off
            if(speed == 0){
                if (player.playSound) // Toggle for enemies wasn't working directly
                {
                    FindObjectOfType<MusicManager>().PlaySoundEffects("Shatter");
                }
                
            }
            else{

                if (player.playSound)
                {
                    FindObjectOfType<MusicManager>().PlaySoundEffects("EnemyDeath");
                }
                
                }
            Destroy(gameObject);
    }

    public void ToggleSFX(bool tog)
    {
        if (tog)
        {
            print("t");
            playSFX = true;
        }
        else
        {
            print("f");
            playSFX=false;
        }
    }
}
