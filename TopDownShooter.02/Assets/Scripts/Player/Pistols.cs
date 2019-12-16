using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;
    public Transform firePoint1;
    public ParticleSystem tracer1;
    public ParticleSystem bulletShell1;
    public Transform firePoint2;
    public ParticleSystem tracer2;
    public ParticleSystem bulletShell2;
    public GameObject shootEffect;
    public GameObject impactEffect;
    private Vector2 target;

    public bool shooting = false;

    private void Start()
    {
        shooting = false;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
        {
            shooting = false;
        }
        if (Input.GetButton("Fire1") && !shooting && Time.timeScale != 0)
        {
            StartCoroutine(ShootingOrder());
        }
    }

    IEnumerator ShootingOrder()
    {
        shooting = true;
        yield return StartCoroutine(checkLineOfSight(firePoint1));
        yield return StartCoroutine(Shoot(firePoint1, tracer1, bulletShell1));  //czeka az skonczy strzelac z pierwszego
        yield return StartCoroutine(checkLineOfSight(firePoint2));
        yield return StartCoroutine(Shoot(firePoint2, tracer2, bulletShell2));  //--||-- z drugiego
        shooting = false;
    }

    IEnumerator checkLineOfSight(Transform firePoint) //rzuca circleCollider i jak dotknelo czegos to przechowuje pozycje w target
    {

        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right, Vector2.Distance(firePoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        if (hitinfo)
        {
            target = hitinfo.point;
        }
        else
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Debug.DrawLine(firePoint.position, target);
        //impactEffect.transform.position = target; nie przenosi na pozycje target

        yield return null;
    }

    IEnumerator Shoot(Transform firePoint, ParticleSystem tracer, ParticleSystem bulletShell)
    {
        //RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right, Vector2.Distance(firePoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))); //sprawdza do pozycji kursora
        //shootFX
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0
        //tracer.Play();  //efekt lecacego pocisku
        bulletShell.Play();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(target, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            Zombie zombie = nearbyObject.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                //  zombie.TakeDamage(damage);
                //zombie.DamageFX / dodaj effekt jak zombie dostaje dmg
            }
        }
        //impactFX
        Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), target, firePoint.rotation);


        yield return new WaitForSeconds(fireRate); //czeka zanim zwroci wartosc do shootingOrder()
    }
    void OnDisable()
    {
        shooting = false;
    }
}
