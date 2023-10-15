using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health = 5;
    public TMP_Text healthDisplay;
    private int oldHealth;

    private Rigidbody2D rb;
    private Vector2 movement;
    public Spawner sp;

    public GameObject hitScreen;
    public GameObject freezeScreen;
    public GameObject windScreen;
    public GameObject fireScreen;

    public GameObject windEffect;
    public GameObject fireEffect;
    
    private ScreenShake ss;

    public float invTime;
    public bool inv = false;
    private float invCounter;

    //Used for dashing:
    public bool canDash = true;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;

    //public bool playSound;

    [SerializeField] private TrailRenderer tr;

    private MusicManager soundEfx;

    void Start()
    {
        ss = GameObject.FindGameObjectWithTag("Shake").GetComponent<ScreenShake>();
        rb = GetComponent<Rigidbody2D>();
        oldHealth = health;
        soundEfx = FindObjectOfType<MusicManager>();

    }

    void Update()
    {
        CheckOverlay(hitScreen);
        CheckOverlay(freezeScreen);
        CheckOverlay(windScreen);
        CheckOverlay(fireScreen);



        if(Input.GetKeyDown(KeyCode.E))
        {
            EffectShake();
            Freeze();
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            EffectShake();
            activateSfx("Wind");
            if(PublicVars.SplashOn)
                Wind();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            activateSfx("Fire");
            if(PublicVars.SplashOn)
                Fire();
        }


        
        if(oldHealth != health)
        {
            if (PublicVars.gotHurtOn)
            {
                GotHurt();
            }
            PlayerDamage();
            //soundEfx.speedUp();
        }
        
        healthDisplay.text = "HEALTH: " + health.ToString();
        if(health <= 0){
            //soundEfx.revert();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (PublicVars.playSFX) //(playSound)
            {
                soundEfx.PlaySoundEffects("PlayerDeath");
            }
            soundEfx.StopBGM();
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = input.normalized * speed;


        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
            activateSfx("Dash");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private IEnumerator Dash(){ //Dash Coroutine
        canDash = false;
        float oldSpeed = speed;
        speed = dashSpeed;
        if(this.gameObject.GetComponent<ColorChange>().x == 0){
            tr.startColor = Color.green;
            tr.endColor = Color.clear;
        }
        else{
            tr.startColor = new Color(0,1f,1f);
            tr.endColor = Color.clear;
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        speed = oldSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    void GotHurt() //ADD TOGGLE
    {
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.3f;
        hitScreen.GetComponent<Image>().color = color;
    }

    void CheckOverlay(GameObject screen)
    {
        if(screen.GetComponent<Image>().color.a > 0)
        {
            var color = screen.GetComponent<Image>().color;
            color.a -= 0.001f;
            screen.GetComponent<Image>().color = color;
        }
    }

    void EffectShake()
    {
        Vector3 oldPos = ss.positionStrength;
        Vector3 oldRot = ss.rotationStrength;
        ss.positionStrength = new Vector3(2f,2f,0f);
        ss.rotationStrength = new Vector3(2f,2f,0f);
        ScreenShake.Invoke();//Toggle this on/off
        ss.positionStrength = oldPos;
        ss.rotationStrength = oldRot;
    }

    void Freeze()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().speed = 0;
            if(PublicVars.ColorsOn)
                enemies[i].GetComponent<ColorChange>().MatFreeze();
        }

        if(PublicVars.SplashOn)
        {
            var color = freezeScreen.GetComponent<Image>().color;
            color.a = 0.3f;
            freezeScreen.GetComponent<Image>().color = color;
        }
        if (PublicVars.playSFX) //(playSound)
        {
            soundEfx.PlaySoundEffects("Freeze");
        }
    }

    void Wind()
    {
        Instantiate(windEffect, transform.position, Quaternion.identity);
        var color = windScreen.GetComponent<Image>().color;
        color.a = 0.3f;
        windScreen.GetComponent<Image>().color = color;
    }

    void Fire()
    {
        Instantiate(fireEffect, transform.position, Quaternion.identity);
        var color = fireScreen.GetComponent<Image>().color;
        color.a = 0.3f;
        fireScreen.GetComponent<Image>().color = color;
    }

    void PlayerDamage()
    {
        if(Camera.main.GetComponent<Camera>().backgroundColor == new Color(0f, 0f, 0f, 0f))
        {
            if(PublicVars.ColorsOn)
                Camera.main.GetComponent<Camera>().backgroundColor = new Color(39f / 255f, 24f / 255f, 49f  / 255f, 0f);
        }
            
        else
        {
            if(PublicVars.ColorsOn)
                Camera.main.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f, 0f);
        }
            
        if(PublicVars.ColorsOn)
            this.GetComponent<ColorChange>().MatChange();
        sp.alt = !sp.alt;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i = 0; i < enemies.Length; i++)
        {
            if(PublicVars.ColorsOn)
                enemies[i].GetComponent<ColorChange>().MatChange();
        }

        oldHealth = health;
        if (PublicVars.playSFX) //(playSound)
        {
            soundEfx.PlaySoundEffects("PlayerHit");
        }
    }

    public void activateSfx(string sfx){
        if(PublicVars.playSFX){
            soundEfx.PlaySoundEffects(sfx);
        }
    }


}
