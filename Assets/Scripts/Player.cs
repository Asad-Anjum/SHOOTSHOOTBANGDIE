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

    //Used for dashing:
    public bool canDash = true;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;

    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldHealth = health;
    }

    void Update()
    {
        if(hitScreen.GetComponent<Image>().color.a > 0)
        {
            var color = hitScreen.GetComponent<Image>().color;
            color.a -= 0.001f;
            hitScreen.GetComponent<Image>().color = color;
        }

        if(freezeScreen.GetComponent<Image>().color.a > 0)
        {
            var color = freezeScreen.GetComponent<Image>().color;
            color.a -= 0.001f;
            freezeScreen.GetComponent<Image>().color = color;
        }



        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().speed = 0;
                enemies[i].GetComponent<ColorChange>().MatFreeze();
            }

            var color = freezeScreen.GetComponent<Image>().color;
            color.a = 0.3f;
            freezeScreen.GetComponent<Image>().color = color;
        }
        if(oldHealth != health)
        {
            GotHurt();
            this.GetComponent<ColorChange>().MatChange();
            sp.alt = !sp.alt;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<ColorChange>().MatChange();
            }

            oldHealth = health;
        }
        healthDisplay.text = "HEALTH: " + health.ToString();
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = input.normalized * speed;


        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
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

    void GotHurt()
    {
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.3f;
        hitScreen.GetComponent<Image>().color = color;
    }
}
