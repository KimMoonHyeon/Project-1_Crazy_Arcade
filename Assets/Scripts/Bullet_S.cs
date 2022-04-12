using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_S : MonoBehaviour
{
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
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Water")
        {
            StartCoroutine("Bullet_Water_Boom", int.Parse(other.gameObject.name));
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
           
        }
        else if(other.gameObject.tag == "Wall")
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
            
        }
        
    }

    IEnumerator Bullet_Water_Boom(int i)
    {

        Player.Water_Empty_List[i].transform.GetChild(0).gameObject.SetActive(false);
        Player.Water_Empty_List[i].transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("Bullet_Water_Particle_Boom", i);
        yield return null;


    }
    IEnumerator Bullet_Water_Particle_Boom(int i)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(Player.Water_Empty_List[i]);
        Destroy(this.gameObject);

    }
}
