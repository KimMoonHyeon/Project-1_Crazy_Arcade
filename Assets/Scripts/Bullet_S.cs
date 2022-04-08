using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_S : MonoBehaviour
{
    public GameObject Bullet;
    private Player_S Player;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player_S>();
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            int a = 0;
            foreach(GameObject water_col in Player.Water_List)
            {
                if(water_col == other.gameObject)
                {
                    Player.Water_List.RemoveAt(a);
                }
                a++;
            }
            
            Destroy(other.gameObject);
            Destroy(Bullet);
        }
        else if(other.gameObject.tag != "Gun")
        {
            Destroy(Bullet);
        }
        
    }
}
