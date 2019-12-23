using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public float fasterSpawns;
    public float harderSpawns;
    public float easierSpawns;
    /*
    public int howManyPerWave;
    public float MobMultiplayer;
    public float timeBtwWaves;
    */

    public float[] chances;
    public GameObject[] enemies;
    public bool[] maxed;
    public Transform[] spawnSpots;
    private bool spawned = false;



    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        Array.Resize(ref maxed, enemies.Length);
        for (int i = 0; i < maxed.Length; i++)  //na start wyzeruj wszystkie maxed
        {
            maxed[i] = false;
        }
    }

    void Update()
    {
        if (Time.timeScale != 0) //tylko jesli nie ma pauzy
        {


            if (timeBtwSpawns <= 0)
            {
                spawned = false;
                while (!spawned && chances[maxed.Length - 1] != 0) //dopoki ostatni zombi bedzie wymaksowany i z za mala szansą to ma 0 szans
                {
                    int randEnemy = UnityEngine.Random.Range(0, enemies.Length); //wybierz losowego enemy
                    float rand = UnityEngine.Random.Range(0f, 1f);
                    if (rand < chances[randEnemy] && chances[randEnemy] != 0) //porownaj z jego szansa na pojawienie sie, sprawdz czy nie jest wymaxowany i nie ma prawie 0 szanse, innaczej 0 szans
                    {
                        int randPos = UnityEngine.Random.Range(0, spawnSpots.Length);
                        Instantiate(enemies[randEnemy], spawnSpots[randPos].position, Quaternion.identity);
                        spawned = true;

                        startTimeBtwSpawns *= fasterSpawns;
                        timeBtwSpawns = startTimeBtwSpawns;
                    }
                }
                // po kazdym spawnie zwieksza szanse na spawn wszystkich zombie
                for (int i = 0; i < chances.Length; i++)
                {
                    if (!maxed[i]) //jesli nie wymaxowane to zwiekszaj szanse do 100%
                    {
                        if (chances[i] < 1)
                        {
                            chances[i] *= harderSpawns;
                        }
                        else if (chances[i] >= 1)
                        {
                            maxed[i] = true;
                        }
                    }
                    else if (maxed[i]) //jesli zombie osiagnal 100% to zmniejszaj do 0 (problem: w koncu wszystkie szanse spadna do 0 i wszystkie zombie przestana sie spawnowac)
                    {
                        if (chances[i] < 0.01) { chances[i] = 0; }  // jak zombi bedzie mial 0 szans to zanczy ze juz byl wymaxowany i zeszla mu szansa od prawie 0
                        else if (chances[i] > 0.01) { chances[i] *= easierSpawns; }
                         
                    }                  
                }

            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
        
    }
}
