using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;
    public Transform firePoint;
    public ParticleSystem flame;

    public bool shooting = false;


    private void Start()
    {
        shooting = false;
    }
    private void Update()
    {
        if (Input.GetButton("Aim")) //sprawdza czy celujesz
        {
                if (Input.GetButton("Fire1") && !shooting && Time.timeScale != 0)
            {
                StartCoroutine(ShootOrder());
            }
        }

    }

    IEnumerator ShootOrder()
    {
        shooting = true;
        yield return StartCoroutine(Shoot());
        shooting = false;
    }
    IEnumerator Shoot()
    {
        flame.Play();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(firePoint.position + (firePoint.right * range), range);
        foreach (Collider2D nearbyObject in colliders)
        {
            Zombie zombie = nearbyObject.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage,nearbyObject.ClosestPoint(firePoint.position));
            }
        }
        yield return new WaitForSeconds(fireRate);
    }
}
