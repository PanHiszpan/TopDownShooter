using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //private Vector2 target;
    public float speed;
    public int damage;


    private void Start()
    {
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //transform.position = Vector2.MoveTowards(transform.position, transform.right, speed * Time.deltaTime);
        transform.position += transform.right * speed;
    }

   void OnTriggerEnter2D(Collider2D collision)
    {

        Zombie zombie = collision.transform.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage, collision.transform.position);
        }
        Destroy(gameObject);
        //impactEffec
        //Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.rotation);

    }
}
