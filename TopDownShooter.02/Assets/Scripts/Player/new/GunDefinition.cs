using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDefinition : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;  //moze zostawic zeby lecialy w neskonczonosc?
    public float angle = 1;
    public int magazineSize;
    public int bulletsLeft;
    public float reloadTime;
    public Transform firePoint;
    public GameObject shootEffect;
    public GameObject impactEffect;
    public ParticleSystem tracer;
    public ParticleSystem bulletShell;

    public bool autoShooting;
    public bool shooting = false;
    public bool reloading = false;

    private SceneMenager sceneMenager;

    //private LineRenderer smokeLine; linerender jest chujowy, idziemy w particle

    private void Start()
    {
        //sceneMenager = FindObjectOfType<SceneMenager>();
        shooting = false;
        bulletsLeft = magazineSize;
        sceneMenager.setAmmo(bulletsLeft);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Reload") && !reloading && Time.timeScale != 0)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetButton("Aim")) //sprawdza czy celujesz
        {
            if (autoShooting)
            {
                if (Input.GetButton("Fire1") && !shooting && !reloading && Time.timeScale != 0 && bulletsLeft > 0)
                {
                    StartCoroutine(ShootOrder());
                }
            }
            else if (!autoShooting)
            {
                if (Input.GetButtonDown("Fire1") && !shooting && !reloading && Time.timeScale != 0 && bulletsLeft > 0)
                {
                    StartCoroutine(ShootOrder());
                }
            }

        }


    }
    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        sceneMenager.setAmmo(bulletsLeft);
        reloading = false;
    }
    protected virtual IEnumerator ShootOrder()
    {
        shooting = true;
        bulletsLeft--;
        sceneMenager.setAmmo(bulletsLeft);
        yield return StartCoroutine(RandomAngle());  
        yield return StartCoroutine(Shoot());
        firePoint.rotation = transform.rotation;  //resetuje rotacje firepointa      
        shooting = false;
    }
    protected virtual IEnumerator RandomAngle()
    {
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward);
        firePoint.rotation = randomTilt * firePoint.rotation;
        yield return null;
    }

    protected virtual IEnumerator Shoot()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right, range);
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0
        tracer.Play(); //za kazdym razem startyje 1 particle
        bulletShell.Play();

        if (hitinfo)
        {
            Zombie zombie = hitinfo.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, hitinfo.point);
            }
            //impactEffec
            Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.rotation);
        }

        yield return new WaitForSeconds(fireRate);
        //tracer.Stop(); //tu konczy??? chyba nie zabardzo, trial zostaje
                         //lepiej jak nie konczy, plynniejsze przejscia i na razie dziala

    }

    void OnEnable()
    {
        sceneMenager = FindObjectOfType<SceneMenager>();
        sceneMenager.setAmmo(bulletsLeft);
    }
    void OnDisable()
    {
        shooting = false;
        reloading = false;
    }
}
