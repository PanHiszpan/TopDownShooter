using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieV2 : MonoBehaviour
{
    public float pointsForKilling;
    public int damage;
    public CircleCollider2D dmgCollider;
    public int health;
    public float speed;
    public float runSpeed;
    public GameObject blood_FX;
    public ParticleSystem killedFX;
    private Transform playerPos;
    private SceneMenager sceneMenager;
    private DropRandom dropRandom;

    //timer
    public float startTimeBtwRC;
    private float timeBtwRC;


    public bool inSight = false;

    public float force;

    void Start()
    {
        sceneMenager = FindObjectOfType<SceneMenager>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        dropRandom = GetComponent<DropRandom>();
        transform.LookAt(playerPos);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);  //obracanie w stronę gracza
    }

    void Update()
    {
        if (inSight)
        {
            transform.LookAt(playerPos);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);  //obracanie w stronę gracza
            transform.position += transform.right * runSpeed * Time.deltaTime;
        }
        else if (!inSight)
        {
            transform.position += transform.right * speed * Time.deltaTime;  // move forward (right in 2d world)
        }
        if (Time.timeScale != 0) //tylko jesli nie ma pauzy
        {
            if (timeBtwRC <= 0)
            {
                //Debug.DrawLine(transform.position, transform.position + (transform.right * 1));
                //sprawdza co jest przed nim
                RaycastHit2D hitinfo = Physics2D.Raycast(transform.position + Vector3.right, transform.right, 1);
                //Zombie zombie = hitinfo.transform.GetComponent<Zombie>();

                if (hitinfo.collider != null)
                {
                    player player = hitinfo.transform.GetComponent<player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
                if (hitinfo.transform.gameObject.layer == 11)
                {
                    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                }
                }



                timeBtwRC = startTimeBtwRC;
            }
            else
            {
                timeBtwRC -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //dodaje punkty do licznika
            sceneMenager.addScore(pointsForKilling);
            //bierze losowe (0-max liczba) dziecko z listy wsystkich dzieci, ustawia na pozycji zombiaka i daje losowa rotacje (zeby wygladaly na rozne)
            Instantiate(blood_FX.transform.GetChild(Random.Range(0, blood_FX.transform.childCount)), transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //Instantiate(killedFX, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
            //dropi bonusy
            dropRandom.Drop();
            //niszczy zombiaka
            Destroy(gameObject); 
        }
    }
    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        if (other.gameObject.layer == 11)
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
         // rozpierdala optymalizajce, bez tego nie jest lepiej, RB wszystko pierdoli, idź moze w circleColider + krotki raycast przd zombi zeby atakowac, wymijac przeszkody
    }
    */

}
