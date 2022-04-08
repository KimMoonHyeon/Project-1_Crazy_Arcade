using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;
    private int bullet_count;
    public List<GameObject> Water_Empty_List = new List<GameObject>();
    // ��ǳ�� �迭�� ����µ�, ũ��� �ϴ� water_length��ŭ �������� ����� �ؾ��ϰ�, 

    public GameObject Water_Empty;
    public GameObject Bullet;

    public int water_max;
    public int water_name;
    public float speed;

    Rigidbody rigid;

    private float water_boom_size;

    void Start()
    {
        water_name = 0;
        water_boom_size = 0;
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
        rigid.velocity = Vector3.zero; //Water�� �浹�� �и� ����, ���ӵ� = 0;

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
            if (Water_Empty_List.Count < water_max)
            {
                // 1,2,3
                water_name++;
                GameObject new_Water_Empty = Instantiate(Water_Empty, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);

                //Instantiate(new_Water_Empty.transform.GetChild(0).gameObject, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);
                new_Water_Empty.name = water_name.ToString();

                //GameObject new_Water_Particle = Instantiate(new_Water_Empty.transform.GetChild(1).gameObject, new Vector3(new_Water_Empty.transform.GetChild(0).gameObject.transform.position.x, new_Water_Empty.transform.GetChild(0).gameObject.transform.position.y, new_Water_Empty.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
                new_Water_Empty.transform.GetChild(1).gameObject.transform.GetChild(0).localScale = new Vector3(1.2f + water_boom_size, 0.3f, 0.3f);
                new_Water_Empty.transform.GetChild(1).gameObject.transform.GetChild(1).localScale = new Vector3(0.3f, 0.3f, 1.2f + water_boom_size);
                new_Water_Empty.transform.GetChild(1).gameObject.SetActive(false);


                Water_Empty_List.Add(new_Water_Empty);
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
        else if (Input.GetKeyDown("4")) //�� �߻��ؼ� ��ǳ�� �Ͷ߸���
        {
            bullet_count += 3; //�ҷ� ī��Ʈ�� �������� �Ծ��� ���� �̵��ϱ�, ���� ���¿��� 4���� ������ �� ���� ���;��ϴϱ�, Input.GetKeyDown(4) && bullet_count >0 ���� ����
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (Input.GetKeyDown("5")) //��ǳ�� ���� �ø���, �ø���� �ǿ���, �ش� ��ǳ���� ��ġ�ϰ� �������� ������,������ �Ա� �� ���̱��� ���� Ŀ���� ����
        {
            water_boom_size += 0.6f;
        }

        if (this.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform Gun_pos = this.gameObject.transform.GetChild(1).gameObject.transform;
                GameObject new_Bullet = Instantiate(Bullet, new Vector3(Gun_pos.position.x, Gun_pos.position.y, Gun_pos.position.z), Quaternion.identity);
                //new_Bullet.GetComponent<Collider>().isTrigger = true;
                Rigidbody bullet_rigid = new_Bullet.GetComponent<Rigidbody>();
                bullet_rigid.velocity = transform.forward * 5;
               
                bullet_count--;
                if (bullet_count <= 0)
                {
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Bullet")
        {
            other.isTrigger = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {   //if�� �־� �б� �������� �Ծ��� ���� �ش� if�� true�� ����� �б� �����ϵ��� �����
        if (collision.gameObject.tag == "Water")
        {
            Rigidbody water_rigid = collision.gameObject.GetComponent<Rigidbody>();
            water_rigid.velocity = move_Vec * 25;
            //�׷��ϱ� ���ͳ����� �浹�� ���ָ� �ǿ���, ����� ���� �ʰ� �ϱ�.
        }
    }

    //�ڷ�ƾ �Լ�
    IEnumerator Water_Boom()
    {
        yield return new WaitForSeconds(30f);

        Water_Empty_List[0].transform.GetChild(0).gameObject.SetActive(false);
        Water_Empty_List[0].transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("Water_Particle_Boom");
    }

    IEnumerator Water_Particle_Boom()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(Water_Empty_List[0]);
        Water_Empty_List.RemoveAt(0);
    }
}
