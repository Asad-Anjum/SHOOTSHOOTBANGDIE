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
    public float waveTime;
    private float timeBtwWaves;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        timeBtwWaves = waveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwWaves <= 0)
        {
            startTimeBtwSpawns-= 0.1f;
            timeBtwWaves = waveTime;
        } 
        else
            timeBtwWaves -= Time.deltaTime;
            
        
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
