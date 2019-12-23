using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBigger : MonoBehaviour
{
    public int timer;
    public int range;

    private void Start()
    {
        StartCoroutine(GetBigger());
    }
    
    IEnumerator GetBigger()
    {
        yield return StartCoroutine(Timer());

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            BloodBigger bloodBigger = nearbyObject.transform.GetComponent<BloodBigger>();
            if (bloodBigger != null)
            {
                Destroy(bloodBigger);
            }
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
    }

   /* private void OnTriggerExit2D(Collider2D other)
    {
        BloodBigger bloodPoolBigger = other.GetComponent<BloodBigger>();
        if (bloodPoolBigger != null)
        {
            Destroy(gameObject);
        }
    }*/
}
