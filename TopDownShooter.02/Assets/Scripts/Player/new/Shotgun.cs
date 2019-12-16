using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun :GunDefinition
{
    public List<GameObject> firePoints = new List<GameObject>();

    protected override IEnumerator RandomAngle()
    {
        for (int i = 0; i < firePoints.Capacity; i++) //jedyna zmiana w stosunku do GunDefinition, wystrzeliwanie kilku pociskow naraz
        {
            Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward);
            firePoints[i].transform.rotation = randomTilt * firePoint.rotation;
        }
        yield return null;
    }

    protected override IEnumerator Shoot()
    {
        //1 bulletshell i 1 shooteffect
        Instantiate(shootEffect.transform.GetChild(Random.Range(0, shootEffect.transform.childCount)), firePoint.position, firePoint.rotation);  //first child have numbr 0
        bulletShell.Play();
        
        for (int i = 0; i < firePoints.Capacity; i++) //jedyna zmiana w stosunku do GunDefinition, wystrzeliwanie kilku pociskow naraz
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(firePoints[i].transform.position, firePoints[i].transform.right); // tak jak wyzej, plus wiele raycastow
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
                Instantiate(impactEffect.transform.GetChild(Random.Range(0, impactEffect.transform.childCount)), hitinfo.point, firePoint.rotation);
            }
        }
        yield return new WaitForSeconds(fireRate);
    }
}
