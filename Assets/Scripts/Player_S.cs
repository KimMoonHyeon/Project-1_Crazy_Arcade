using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;
    public List<GameObject> Water_List = new List<GameObject>();

    // ��ǳ�� �迭�� ����µ�, ũ��� �ϴ� water_length��ŭ �������� ����� �ؾ��ϰ�, 

    public GameObject Water;

    public int water_max;
    public float speed;

    Rigidbody rigid;

    void Start()
    {
        water_max = 3;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Plyaer_Move();
        Plyaer_Skiil(); 
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

    }

    void Plyaer_Skiil()
    {
        //space -> ��ǳ�� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(Water_List.Count);
            if (Water_List.Count < water_max)
            {
                // 1,2,3
                GameObject new_Water = Instantiate(Water, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);
                Water_List.Add(new_Water);
                new_Water.gameObject.GetComponent<Collider>().isTrigger = true;
                StartCoroutine("Water_Boom");
                //���� ��, 3d ť��� �� �� �����غ��� Trigger �̿��ؼ� �浹����
            }
            
        }
        else if (Input.GetKeyDown("1")) //���ǵ� ����
        {
            speed += 3f;
        }
        else if (Input.GetKeyDown("2")) //�б�
        {

        }
        else if (Input.GetKeyDown("3")) //��ǳ�� ���� �߰�
        {
            water_max++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }
    OnCollision();
    

    //�ڷ�ƾ �Լ�
    IEnumerator Water_Boom()
    {
        yield return new WaitForSeconds(3f);
        Destroy(Water_List[0]);
        Water_List.RemoveAt(0);
    }
}
