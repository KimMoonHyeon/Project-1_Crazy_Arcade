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
        //plyaer �̵�
        this.transform.position += move_Vec.normalized * speed * Time.deltaTime;

        //player ȸ��
        if(m_z != 0 || m_x != 0)
            this.transform.rotation = Quaternion.LookRotation(new Vector3(m_x,0,m_z));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���ڸ����� space ���� ��쿡�� Trigger �����������.
            Debug.Log("��ǳ�� �߻�");

        }

    }


}
