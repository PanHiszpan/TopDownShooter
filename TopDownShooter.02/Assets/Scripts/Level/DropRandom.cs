using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRandom : MonoBehaviour
{
    public float chance;
    public GameObject[] dropList;

    public void IfDrop()  //losuje czy dropnac
    {
        float rand = Random.Range(0f, 1f);
        if (rand < chance)
        {
            drop();
        }
    }
    public void drop()  //dropi losowe z listy 
    {
        int rand = Random.Range(0, dropList.Length);
        Instantiate(dropList[rand], transform.position, Quaternion.identity);
    }
}
