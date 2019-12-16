using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    public int health;
    public float InvAfterDmg;
    public float speed;
    public float rotationSpeed;
    public float slowSpeed;
    private float originalSpeed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public Vector2 facingVector;
    private SceneMenager sceneMenager;

    public bool invulnerable = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sceneMenager = FindObjectOfType<SceneMenager>();
        sceneMenager.setHealth(health);
        originalSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetButton("Aim"))
        {
            //slow movement
            speed = slowSpeed;

            //lookat() nie chce dzialac
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;  //obracanie postaci w stronę kursora
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, 0f, rotZ);     //koniec
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            /*
            transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            */
        }
        else if (Input.GetButtonUp("Aim"))
        {
            speed = originalSpeed;
        }



        //moving
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(moveInput.magnitude != 0)
        {
            facingVector = moveInput;
        }
        if (!Input.GetButton("Aim"))
        {
            Vector3 difference = new Vector3(facingVector.x, facingVector.y, 0);  //obracanie postaci w stronę fac
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, 0f, rotZ);     //koniec
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            //transform.right = moveInput;
        }
        
        moveVelocity = moveInput.normalized * speed;
     }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveVelocity * Time.fixedDeltaTime));
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            StartCoroutine(takingDamage(damage));
        }
    }
    IEnumerator takingDamage(int damage)
    {
        invulnerable = true;

        health -= damage;
        sceneMenager.setHealth(health);

        if (health <= 0)
        {
            sceneMenager.deathScreen();
        }
        yield return new WaitForSeconds(InvAfterDmg); //przez jakis czas niesmiertelnosc
        invulnerable = false;
    }
    public void takeHealth(int healthUp)
    {
        health += healthUp;
        sceneMenager.setHealth(health);
    }
}
