using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_S : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water_Particle")
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
