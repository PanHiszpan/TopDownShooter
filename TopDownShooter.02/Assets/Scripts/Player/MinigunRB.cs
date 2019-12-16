using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunRB : MonoBehaviour
{
    //public int damage;
    public float fireRate;
    //public float range;  //moze zostawic zeby lecialy w neskonczonosc?
    public int magazineSize;
    public int bulletsLeft;
    public float reloadTime;
    public GameObject bullet;
    public Transform firePoint;
    public GameObject shootEffect;
    //public GameObject impactEffect;
    //public ParticleSystem tracer;
    public ParticleSystem bulletShell;

    public float maxAngle;
    public float angle = 1;
    public int shotsTilMaxAngle;
    public int bulletsFired = 1;

    public bool shooting = false;
    public bool reloading = false;

    //private LineRenderer smokeLine; linerender jest chujowy, idziemy w particle

    private void Start()
    {
        shooting = false;
        bulletsLeft = magazineSize;
        angle = 1;
        bulletsFired = 1;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Reload") && !reloading && Time.timeScale != 0)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetButton("Fire1") && !shooting && !reloading && Time.timeScale != 0 && bulletsLeft > 0)
        {
            StartCoroutine(ShootOrder());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            bulletsFired = 1; //zeruje rozrzut, (czasami naliczy 1 pocisk), zeruje do 1 bo 0 przy wymnazaniu zawsze zeruje kat
            angle = 1;
        }

    }
    IEnumerator Reload()
    {
        reloading = true;
        bulletsFired = 1; //zeruje rozrzut
        angle = 1; //zeby po skonczeniu seri nie zatrzymywal sie na danym kacie (w sumie nie potrzebne bo bulletsfired resetuje tez angle przy perwszym strzale)
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        reloading = false;
    }
    IEnumerator ShootOrder()
    {
        shooting = true;
        yield return StartCoroutine(RandomAngle());
        yield return StartCoroutine(Shoot());
        bulletsFired++;
        bulletsLeft--;
        firePoint.rotation = transform.rotation;
        shooting = false;
    }
    IEnumerator RandomAngle()
    {
        if (bulletsFired > shotsTilMaxAngle) { angle = maxAngle; }
        else { angle = (((float)bulletsFired / (float)shotsTilMaxAngle) * maxAngle); }
        //np. mnozy 3 pociski wystrzelone/ 10 do full rozrzutu = 30% zakresu rozrzutu przy losowaniu kata
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward);
        firePoint.rotation = randomTilt * firePoint.rotation;
        yield return null;
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0
        //tracer.Play(); //za kazdym razem startyje 1 particle
        bulletShell.Play();

        Instantiate(bullet, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(fireRate);
        //tracer.Stop(); //tu konczy??? chyba nie zabardzo, trial zostaje
        //lepiej jak nie konczy, plynniejsze przejscia i na razie dziala

    }
    void OnDisable()
    {
        shooting = false;
        reloading = false;
        bulletsFired = 1;
        angle = 1;
    }
}

