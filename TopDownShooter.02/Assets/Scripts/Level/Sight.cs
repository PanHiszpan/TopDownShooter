using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private ZombieV2 zombiev2;

    private void Start()
    {
        zombiev2 = GetComponentInParent<ZombieV2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            zombiev2.inSight = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            zombiev2.inSight = false;
        }
    }
}
