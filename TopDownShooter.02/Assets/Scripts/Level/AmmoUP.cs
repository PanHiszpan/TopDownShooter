using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUP : PickupDefinition
{
    public int gunNumber;
    public int ammo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(pickupText); //zrob text nad pickupem, pare sec i znika
        GunDefinition gunDef = collision.transform.GetChild(gunNumber).GetComponent<GunDefinition>();
        gunDef.ammoAll += ammo;
        if (gunDef.isActiveAndEnabled) //zeby nie wyswietlal ammoAll np. miniguna jak jest wybrany pistol
        {
            gunDef.PickedAmmo();
        }
    }

}
