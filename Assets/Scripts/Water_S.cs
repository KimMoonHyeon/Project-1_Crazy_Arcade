using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_S : MonoBehaviour
{
    Rigidbody water_rigid;
    public GameObject Water_obj;
    void Start()
    {
        water_rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Water")
        {
            water_rigid.isKinematic = true;
            Debug.Log(collision.gameObject.name);
        }
    }

}
