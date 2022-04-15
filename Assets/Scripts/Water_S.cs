using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_S : MonoBehaviour
{
    Rigidbody water_rigid;
    private Player_S Player;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player_S>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            StartCoroutine("Obstacle_Water_Boom", int.Parse(this.gameObject.name));
            water_rigid.velocity = Vector3.zero;
        }
    }

    IEnumerator Obstacle_Water_Boom(int i)
    {

        Player.Water_Empty_List[i].transform.GetChild(0).gameObject.SetActive(false);
        Player.Water_Empty_List[i].transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("Obstacle_Water_Particle_Boom", i);
        yield return null;


    }
    IEnumerator Obstacle_Water_Particle_Boom(int i)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(Player.Water_Empty_List[i]);
        Destroy(this.gameObject);

    }

}
