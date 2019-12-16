using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    public int currentWeapon;
    public List<GameObject> weapon;
    public List<GameObject> weaponOld;

    public bool old;
    //odwolania do skryptow
    //private AreaGun areaGun;


    private void Start()
    {
        weapon[0].SetActive(true);
        for (int i = 1; i < weapon.Capacity; i++) { weapon[i].SetActive(false); } //wylacza reszte broni
        currentWeapon = 0;
        for (int i = 0; i < weaponOld.Capacity; i++) { weaponOld[i].SetActive(false); } //wylacza reszte broni
    }

    private void Update()
    { //dodawaj nowe przyciski
        if (Input.GetKeyDown("q"))
        {
            if (!old)
            {
                old = true;
                weapon[currentWeapon].SetActive(false);
                weaponOld[currentWeapon].SetActive(true);
            }
            else if (old)
            {
                old = false;
                weaponOld[currentWeapon].SetActive(false);
                weapon[currentWeapon].SetActive(true);
            }
            
        }

        if (old)
        {
            if (Input.GetButtonUp("Weapon_1") && !Input.GetButton("Fire1") && currentWeapon != 0) { ChangeWeapon(0); }
            if (Input.GetButtonUp("Weapon_2") && !Input.GetButton("Fire1") && currentWeapon != 1) { ChangeWeapon(1); }
            if (Input.GetButtonUp("Weapon_3") && !Input.GetButton("Fire1") && currentWeapon != 2) { ChangeWeapon(2); }
            /*
            if (Input.GetButtonUp("Weapon_4") && !Input.GetButton("Fire1") && currentWeapon != 3) { ChangeWeapon(3); }
            if (Input.GetButtonUp("Weapon_5") && !Input.GetButton("Fire1") && currentWeapon != 4) { ChangeWeapon(4); }
            if (Input.GetButtonUp("Weapon_6") && !Input.GetButton("Fire1") && currentWeapon != 5) { ChangeWeapon(5); }
            if (Input.GetButtonUp("Weapon_7") && !Input.GetButton("Fire1") && currentWeapon != 6) { ChangeWeapon(6); }
            if (Input.GetButtonUp("Weapon_8") && !Input.GetButton("Fire1") && currentWeapon != 7) { ChangeWeapon(7); }
            if (Input.GetButtonUp("Weapon_9") && !Input.GetButton("Fire1") && currentWeapon != 8) { ChangeWeapon(8); }
            if (Input.GetButtonUp("Weapon_10") && !Input.GetButton("Fire1") && currentWeapon != 9) { ChangeWeapon(9); }
            */        
        }
        else
        {
            if (Input.GetButtonUp("Weapon_1") && !Input.GetButton("Fire1") && currentWeapon != 0) { ChangeWeapon(0); }
            if (Input.GetButtonUp("Weapon_2") && !Input.GetButton("Fire1") && currentWeapon != 1) { ChangeWeapon(1); }
            if (Input.GetButtonUp("Weapon_3") && !Input.GetButton("Fire1") && currentWeapon != 2) { ChangeWeapon(2); }
            if (Input.GetButtonUp("Weapon_4") && !Input.GetButton("Fire1") && currentWeapon != 3) { ChangeWeapon(3); }
            if (Input.GetButtonUp("Weapon_5") && !Input.GetButton("Fire1") && currentWeapon != 4) { ChangeWeapon(4); }
            if (Input.GetButtonUp("Weapon_6") && !Input.GetButton("Fire1") && currentWeapon != 5) { ChangeWeapon(5); }
            /*
            if (Input.GetButtonUp("Weapon_7") && !Input.GetButton("Fire1") && currentWeapon != 6) { ChangeWeapon(6); }
            if (Input.GetButtonUp("Weapon_8") && !Input.GetButton("Fire1") && currentWeapon != 7) { ChangeWeapon(7); }
            if (Input.GetButtonUp("Weapon_9") && !Input.GetButton("Fire1") && currentWeapon != 8) { ChangeWeapon(8); }
            if (Input.GetButtonUp("Weapon_10") && !Input.GetButton("Fire1") && currentWeapon != 9) { ChangeWeapon(9); }
            */
        }

    }

    private void ChangeWeapon(int nr)
    {
        if (old)
        {
            weaponOld[currentWeapon].SetActive(false);
            weaponOld[nr].SetActive(true);
            currentWeapon = nr;
        }
        else
        {
            weapon[currentWeapon].SetActive(false);
            weapon[nr].SetActive(true);
            currentWeapon = nr;
        }
        //areaGun =  weapon[currentWeapon].GetComponent<AreaGun>(); //odwolanie do każdego skryptu zrob, ale nie wiem jeszcze jakie beda xd
        //areaGun.shooting = false;

    }
}
