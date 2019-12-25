using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieRB : MonoBehaviour {

    /// <summary>
    /// /Old zombie script with rigidbody colliders, cant bump more then 100 witth each other :/
    /// </summary>
    public float pointsForKilling;
    public int damage;
    public int health;
    public float speed;
    public GameObject blood_FX;
    private Transform playerPos;
    //private player player;
    private SceneMenager sceneMenager;
    private DropRandom dropRandom;
    

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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //dodaje punkty do licznika
            sceneMenager.addScore(pointsForKilling);
            //bierze losowe (0-max liczba) dziecko z listy wsystkich dzieci, ustawia na pozycji zombiaka i daje losowa rotacje (zeby wygladaly na rozne)
            Instantiate(blood_FX.transform.GetChild(Random.Range(0, blood_FX.transform.childCount)), transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //dropi bonusy
            dropRandom.Drop();
            //niszczy zombiaka
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        player player = hitinfo.GetComponent<player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
