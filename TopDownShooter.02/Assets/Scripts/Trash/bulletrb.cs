using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletrb : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    


    void Start()
    {
        Invoke("DestroyProjecticle", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjecticle()
    {
        Destroy(gameObject);
    }
}
