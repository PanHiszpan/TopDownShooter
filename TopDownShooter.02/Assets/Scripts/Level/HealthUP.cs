using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUP : PickupDefinition
{
    public int healthUp;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            Debug.Log(pickupText); //zrob text nad pickupem, pare sec i znika
            player.takeHealth(healthUp);
        }
    }
}
