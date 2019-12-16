using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float range;
    public float slowRotationSpeed;
    private float orinalRotationSpeed;
    //public float slowSpeed;
    //private float originalSpeed;
    public Transform firePoint;
    public GameObject shootEffect;
    public GameObject impactEffect;

    public bool shooting = false;
    private bool slow = false;

    private LineRenderer laserLine;
    private player player;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        player = GetComponentInParent<player>();

        shooting = false;
        orinalRotationSpeed = player.rotationSpeed;
        //originalSpeed = player.speed; speed do wyjebania
    }
    private void Update()
    {
        if (Time.timeScale != 0)  //tylko jak czas leci normalnie
        {
            if (Input.GetButton("Aim")) //sprawdza czy celujesz
            {
                if (Input.GetButton("Fire1") && !shooting)  //strzela
                {
                    if (!slow) //spowalnia tylko raz, potem omija ta czesc
                    {
                        player.rotationSpeed = slowRotationSpeed;
                        //player.speed = slowSpeed;
                        laserLine.enabled = true;
                        slow = true;
                    }
                    StartCoroutine("Shoot");
                }
            }

            if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Aim") || Input.GetButtonDown("Cancel"))  //resetuje bron jak pusci fire1 lub jak wyjdzie z pauseMenu, nie wiem czemu dziala, najpierw zmienia czas na 1 potem sprawdza???
            {
                ResetLaserGun();
            }
        }

    }


    IEnumerator Shoot()
    {
        shooting = true;
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        //shootEffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0

        laserLine.SetPosition(0, firePoint.position);

        if (hitinfo)
        {
            laserLine.SetPosition(1, hitinfo.point);

            Zombie zombie = hitinfo.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, hitinfo.point);
            }
            //impactEffec
            Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.rotation);
        }
        else
        {
            laserLine.SetPosition(1, firePoint.position + (transform.right * range));
        }

        
        yield return new WaitForSeconds(fireRate);

        shooting = false;

    }

    public void ResetLaserGun()
    {
        player.rotationSpeed = orinalRotationSpeed;
        //player.speed = originalSpeed;
        laserLine.enabled = false;
        slow = false;
    }
    void OnDisable()
    {
        shooting = false;
        ResetLaserGun();
    }
}
