using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_S : MonoBehaviour
{
    //public GameObject Bullet;
    private Player_S Player;
    //private GameObject Bullet;


    void Start()
    {
        //Bullet = GameObject.Find("Player").GetComponent<Player_S>().Bullet;
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
            Debug.Log("����");
            for (int i = Player.Water_Empty_List.Count-1; i >=0; i--) {
                if(Player.Water_Empty_List[i].name == other.gameObject.name)
                {
                    Debug.Log("i:"+i);
                    StartCoroutine("Water_Boom", i);
                }
            }

            Destroy(this);
        }
        else if(other.gameObject.tag == "Wall")
        {
            Debug.Log("�� ����ϱ� �����ʿ�");
            Destroy(this);
        }
        
    }

    //�ڷ�ƾ �Լ�

    IEnumerator Water_Boom(int i)
    {
        
        Player.Water_Empty_List[i].transform.GetChild(0).gameObject.SetActive(false);
        Player.Water_Empty_List[i].transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("Water_Particle_Boom",i);
        Debug.Log("!!??");
        yield return null;
        Debug.Log("!!??!!!!!!!!!!!");

    }
    IEnumerator Water_Particle_Boom(int i)
    {
        Debug.Log("�Ⱦ�����.....?");
        yield return new WaitForSeconds(0.3f);
        Debug.Log("�Ⱦ�����?");
        Destroy(Player.Water_Empty_List[i]);
        Player.Water_Empty_List.RemoveAt(i);
    }

}
