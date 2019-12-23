using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingRC : MonoBehaviour
{

   
    public int damage;
    public float fireRate;
    public float waitBeforeShooting;
    public Transform firePoint;
    public GameObject shootEffect;
    public GameObject impactEffect;

    public bool shooting = false;

    private void Start()
    {
        shooting = false;  
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !shooting && Time.timeScale != 0)
        {
            StartCoroutine("Shoot");
        }
    }
    

    IEnumerator Shoot()
    {
        
        shooting = true;
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0,shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0


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
        shooting = false;

    }
}
