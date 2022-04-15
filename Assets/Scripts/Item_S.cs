using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_S : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
