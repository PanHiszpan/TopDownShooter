using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunOld : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;
    public GameObject firePoint;
    public int bulletsPerShot;
    public List<GameObject> firePoints = new List<GameObject>();

    public GameObject shootEffect;
    public GameObject impactEffect;
    //public ParticleSystem tracer;
    public ParticleSystem bulletShell;

    public float conicAngle;

    public bool shooting = false;


    private void Start()
    {
        shooting = false;
        //firePoints = new List<GameObject>(bulletsPerShot);
        //Debug.Log(firePoints.Capacity);
        //for (int i = 0; i < firePoints.Capacity; i++) { firePoints[i] = firePoint; } //przypisz pos do private firePointow
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
        yield return StartCoroutine(RandomAngle());
        yield return StartCoroutine(Shoot());
        //for (int i = 0; i < firePoints.Capacity; i++) { firePoints[i] = firePoint; } to wszystko psulo???

        shooting = false;
    }
    IEnumerator RandomAngle()
    {
        for (int i = 0; i < firePoints.Capacity; i++)  //wylosuj dla kazdego FP
        {
            Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-conicAngle, conicAngle), Vector3.forward);
            firePoints[i].transform.rotation = randomTilt * firePoint.transform.rotation;
        }
        yield return null;
    }

    IEnumerator Shoot()
    {
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.transform.position, firePoint.transform.rotation);  //first child have numbr 0
        
        bulletShell.Play();

        for (int i = 0; i < firePoints.Capacity; i++)
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(firePoints[i].transform.position, firePoints[i].transform.right);
            //tracers
            firePoints[i].GetComponentInChildren<ParticleSystem>().Play();
            if (hitinfo)
            {
                Zombie zombie = hitinfo.transform.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage, hitinfo.point);
                }
                //impactEffec
                Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.transform.rotation);
            }
        }
        yield return new WaitForSeconds(fireRate);
    }

    void OnDisable()
    {
        shooting = false;
    }
}