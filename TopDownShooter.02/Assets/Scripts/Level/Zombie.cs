using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float pointsForKilling;
    public int damage;
    public int health;
    public float speed;
    //public GameObject blood_FX;
    public ParticleSystem killedFX;
    private Transform playerPos;
    //private player player;
    private SceneMenager sceneMenager;
    private DropRandom dropRandom;

    public float force;

    void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<player>();
        sceneMenager = FindObjectOfType<SceneMenager>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        dropRandom = GetComponent<DropRandom>();
    }

    void Update()
    {
        transform.LookAt(playerPos);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);  //obracanie w stronę gracza

        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime); //poruszanie sie w strone gracza
    }

    public void TakeDamage(int damage, Vector2 hitinfo)
    {
        health -= damage;

        if (health <= 0)
        {
            //dodaje punkty do licznika
            sceneMenager.addScore(pointsForKilling);
            //[niepotrzebane, particle lepsze]bierze losowe (0-max liczba) dziecko z listy wsystkich dzieci, ustawia na pozycji zombiaka i daje losowa rotacje (zeby wygladaly na rozne)
            //Instantiate(blood_FX.transform.GetChild(Random.Range(0, blood_FX.transform.childCount)), transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //oblicza kierunek z którego dostał obrażenia, vector skierowany chyba(?) do wewnątrz
            Vector2 dir = (Vector2)transform.position - hitinfo;
            //spawnuje particle
            Instantiate(killedFX, transform.position + new Vector3(0,0,3), Quaternion.LookRotation(dir));
            //dropi bonusy
            dropRandom.IfDrop();
            //niszczy zombiaka
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player player = collision.GetComponent<player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
    void OnTriggerEnter2D(Collider2D collison)
    {

        Zombie zombieSimple = collison.GetComponent<Zombie>();
        if (zombieSimple != null)
        {
            transform.position =  Vector2.MoveTowards(transform.position, zombieSimple.transform.position,  -force * Time.deltaTime);
        }

    }
}
