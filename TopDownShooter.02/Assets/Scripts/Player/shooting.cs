using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject shot;
    public float startShootTime;
    public float offset;
    private float shootTime;
    private Transform shotPoint;
    private Transform target;


    void Start()
    {
        shotPoint = GetComponent<Transform>();
        shootTime = 0;
        target.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;  //obracanie postaci w stronę kursora
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);                                      //koniec
*/


        if (Input.GetMouseButton(0))
        {
            
            
            if (shootTime < 0)
            {
                Instantiate(shot, shotPoint.position, transform.rotation);
                shootTime = startShootTime;
            }
            else
            {
                shootTime -= Time.deltaTime;
            }
        }
    }
}
