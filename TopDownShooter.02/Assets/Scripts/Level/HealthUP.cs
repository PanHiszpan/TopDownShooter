﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUP : MonoBehaviour
{
    public int healthUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            player.takeHealth(healthUp);
        }
    }
}
