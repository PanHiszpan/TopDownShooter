using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGunRC : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;
    private Vector2 target;
    public Transform firePoint;
    public GameObject shootEffect;
    public ParticleSystem impactEffect;
    // public ParticleSystem tracer;
    public ParticleSystem bulletShell;

    private LineRenderer laserLine;

    public bool shooting = false;

    private void Start()
    {
        shooting = false;
        laserLine = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (Input.GetButton("Aim")) //sprawdza czy celujesz
        {
            if (Input.GetButton("Fire1") && !shooting && Time.timeScale != 0)
            {
                StartCoroutine(ShootOrder());
            }
            //impactEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

    }

    IEnumerator ShootOrder()
    {
        shooting = true;
        yield return StartCoroutine(checkLineOfSight());
        yield return StartCoroutine(Shoot());
        laserLine.enabled = false;
        shooting = false;

    }


    IEnumerator checkLineOfSight() //rzuca circleCollider i jak dotknelo czegos to przechowuje pozycje w target
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
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, target);
        Debug.DrawLine(firePoint.position, target);
        impactEffect.transform.position = target;
        yield return null;
    }


    IEnumerator Shoot()
    {
        laserLine.enabled = true;
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have number 0
        bulletShell.Play();
        //impactFX
        impactEffect.Play();
        //Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), target, firePoint.rotation);
        

        Collider2D[] colliders = Physics2D.OverlapCircleAll(target, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            Zombie zombie = nearbyObject.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, nearbyObject.ClosestPoint(target));
                //zombie.DamageFX / dodaj effekt jak zombie dostaje dmg
            }
        }


        yield return new WaitForSeconds(fireRate);


    }
    void OnDisable()
    {
        laserLine.enabled = false;
        shooting = false;
    }
}
