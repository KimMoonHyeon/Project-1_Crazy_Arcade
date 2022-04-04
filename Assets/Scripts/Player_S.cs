using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;

    public float speed;

    Rigidbody rigid;

    void Start()
    {
        

        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Plyaer_Move();
    }


    void Plyaer_Move()
    {
        float m_x = Input.GetAxisRaw("Horizontal");
        float m_z = Input.GetAxisRaw("Vertical");

        move_Vec = new Vector3(m_x, 0, m_z);
        //plyaer 이동
        this.transform.position += move_Vec.normalized * speed * Time.deltaTime;

        //player 회전
        if(m_z != 0 || m_x != 0)
            this.transform.rotation = Quaternion.LookRotation(new Vector3(m_x,0,m_z));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //제자리에서 space 누를 경우에는 Trigger 해제해줘야지.
            Debug.Log("물풍성 발사");

        }

    }


}
