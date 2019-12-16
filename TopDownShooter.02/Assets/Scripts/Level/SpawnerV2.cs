using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerV2 : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public float fasterSpawns;
    
    public int enemyCount;
    private int enemyCounter; 
    
    public GameObject enemy;
    public Transform[] spawnSpots;




    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        enemyCounter = enemyCount;
    }

    void Update()
    {
        if (enemyCount > 0)
        {
            if (Time.timeScale != 0) //tylko jesli nie ma pauzy
            {
                if (timeBtwSpawns <= 0)
                {
                    int randPos = Random.Range(0, spawnSpots.Length);
                    Instantiate(enemy, spawnSpots[randPos].position, Quaternion.Euler(0, 0, Random.Range(0,360))); //losowa rotacja żeby chodzili w różną stronę

                    enemyCount--;

                    startTimeBtwSpawns *= fasterSpawns; 
                    timeBtwSpawns = startTimeBtwSpawns;
                }
                else
                {
                    timeBtwSpawns -= Time.deltaTime;
                }
            }
        }


    }
}
