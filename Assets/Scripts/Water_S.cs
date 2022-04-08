using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_S : MonoBehaviour
{
    Rigidbody water_rigid;
    void Start()
    {
        water_rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
 
        if(collision.gameObject.tag =="Water" || collision.gameObject.tag == "Wall")
        {
            water_rigid.isKinematic = true;
        }
    }

}
