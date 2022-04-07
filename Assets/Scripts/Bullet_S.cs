using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_S : MonoBehaviour
{
    public GameObject Bullet;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Gun")
        {
            Destroy(Bullet);
        }
    }
}
