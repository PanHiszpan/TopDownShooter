using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRandom : MonoBehaviour
{
    //wyzej na liscie ma pierwszenstw oprzy spawnie (np spawn nr2 konczy sprawdzanie)
    public GameObject[] dropList;


    public void Drop()  //dropi losowe z listy 
    {       
        //leci przez cala liste i sprawdza czy dropnie
        for (int i = 0; i < dropList.Length; i++)
        {
            float rand = Random.Range(0f, 1f); //losuje 0-1
            if (rand < dropList[i].GetComponent<PickupDefinition>().chanceToDrop) //pobiera chanceToDrop z dziedziczonej klasy GunDefinition dla każdego GO z listy, zajebiscie
            {
                Instantiate(dropList[i], transform.position, Quaternion.identity); //spawnuje dany pickup z listy
                break; // pilnuje zeby spawnowal tylko 1
            }
        }
    }
}
