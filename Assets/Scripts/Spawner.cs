using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    
    public GameObject enemy;
    public GameObject enemy2;
    public Transform[] spawnPoints;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public bool alt = false;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeBtwSpawns <= 0)
        {
            int point = Random.Range(0, spawnPoints.Length);
            if(!alt || !PublicVars.ColorsOn)
                Instantiate(enemy, spawnPoints[point].position, Quaternion.identity);
            else
                Instantiate(enemy2, spawnPoints[point].position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
            timeBtwSpawns -= Time.deltaTime;
    }
}
