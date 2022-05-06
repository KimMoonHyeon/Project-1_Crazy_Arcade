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
        if (other.gameObject.tag == "Water_Particle")
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        if ( (other.tag == "Player" && this.transform.GetChild(0).gameObject.activeSelf == false) || (other.tag == "Player" && this.transform.childCount == 0) )
        {
            Destroy(this.gameObject);
        }
    
    }


}