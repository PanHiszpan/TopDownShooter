using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnContact : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            Destroy(gameObject);
        }
    }
}
