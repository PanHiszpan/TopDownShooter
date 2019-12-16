using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDMG : MonoBehaviour
{
    private ZombieV2 zombiev2;

    private void Start()
    {
        zombiev2 = GetComponentInParent<ZombieV2>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            player.TakeDamage(zombiev2.damage);
        }
    }
}
