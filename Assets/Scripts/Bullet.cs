using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    public float destroyTimer;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform. position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, rot+90);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > destroyTimer) Destroy(gameObject);
    }


}
