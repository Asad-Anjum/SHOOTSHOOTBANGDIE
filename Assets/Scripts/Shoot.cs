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

    void Start()
    {
        playerPos = GetComponent<Transform>();
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
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
