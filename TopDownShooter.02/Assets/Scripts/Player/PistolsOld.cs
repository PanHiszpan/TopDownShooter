using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolsOld : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public Transform firePoint1;
    public ParticleSystem tracer1;
    public ParticleSystem bulletShell1;
    public Transform firePoint2;
    public ParticleSystem tracer2;
    public ParticleSystem bulletShell2;
    public GameObject shootEffect;
    public GameObject impactEffect;

    public bool shooting = false;
    public bool pistol1 = true;

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
                StartCoroutine(ShootingOrder());
            }
        }

    }

    IEnumerator ShootingOrder()
    {
        shooting = true;
        if (pistol1) { yield return StartCoroutine(Shoot(firePoint1, tracer1, bulletShell1)); }  //czeka az skonczy strzelac z pierwszego
        else        { yield return StartCoroutine(Shoot(firePoint2, tracer2, bulletShell2)); }   //--||-- z drugiego 
        pistol1 = !pistol1;
        shooting = false;
    }

    IEnumerator Shoot(Transform firePoint, ParticleSystem tracer, ParticleSystem bulletShell)
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0
        tracer.Play();  //efekt lecacego pocisku
        bulletShell.Play();

        if (hitinfo)
        {
            Zombie zombie = hitinfo.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, hitinfo.point);
            }
            //impactEffect
            Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.rotation);
        }

        yield return new WaitForSeconds(fireRate); //czeka zanim zwroci wartosc do shootingOrder()
    }
    void OnDisable()
    {
        shooting = false;
    }
}
