using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;
    private int water_name;

    private int bullet_count; //�Ѿ� ���� ��ų
    private int water_max; //��ǳ�� ���� �ø��� ��ų
    private int barrier_time;
    private float speed; // ���ǵ�� ��ų
    private float water_boom_size; //��ǳ�� ���� ���� ��ų


    public List<GameObject> Water_Empty_List = new List<GameObject>();
    public GameObject Water_Empty;
    public GameObject Bullet;

    Rigidbody rigid;

    void Start()
    {
        speed = 5;
        barrier_time = 0;
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
            
            GameObject new_Water_Empty = Instantiate(Water_Empty, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);

            new_Water_Empty.name = water_name.ToString();
               
            new_Water_Empty.transform.GetChild(1).gameObject.transform.GetChild(0).localScale = new Vector3(1.2f + water_boom_size, 0.3f, 0.3f);
            new_Water_Empty.transform.GetChild(1).gameObject.transform.GetChild(1).localScale = new Vector3(0.3f, 0.3f, 1.2f + water_boom_size);
            new_Water_Empty.transform.GetChild(1).gameObject.SetActive(false);


            Water_Empty_List.Add(new_Water_Empty);
            StartCoroutine("Non_Attack", new_Water_Empty.name);
            water_name++;
            
            
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
        else if (Input.GetKeyDown("6"))
        {
            barrier_time += 3;
            if (this.transform.GetChild(2).gameObject.activeSelf == false)
            {
                StartCoroutine("Barrier_TIme");
            }
}
        if (this.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform Gun_pos = this.gameObject.transform.GetChild(1).gameObject.transform;
                GameObject new_Bullet = Instantiate(Bullet, new Vector3(Gun_pos.position.x, Gun_pos.position.y, Gun_pos.position.z), Quaternion.identity);
          
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water_Particle" && barrier_time ==0)
        {
            Debug.Log("����!");

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
    IEnumerator Water_Boom(int a)
    {
        Debug.Log("Water_Boom_1");
        yield return null;
        if (GameObject.Find(a.ToString()) == true)
        {
            Water_Empty_List[a].transform.GetChild(0).gameObject.SetActive(false);
            Water_Empty_List[a].transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine("Water_Particle_Boom", a);
        }

    }

    IEnumerator Water_Particle_Boom(int a)
    {
        
        yield return new WaitForSeconds(0.2f);
        if (GameObject.Find(a.ToString()) == true)
        {
            Destroy(Water_Empty_List[a]);
        }
        
    }

    IEnumerator Non_Attack(string a)
    {
        int b = int.Parse(a);
        yield return new WaitForSeconds(2f);
        if (GameObject.Find(a) == true)
        {
            StartCoroutine("Water_Boom", b);
        }

    }

    IEnumerator Barrier_TIme() //�ߺ��ؼ� ������ �� �ְ� ��������, 
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        while (barrier_time > 0)
        {
            Debug.Log("�� ���ð�:" + barrier_time);
            yield return new WaitForSeconds(1f);
            barrier_time--;
        }
        if (barrier_time <= 0)
        {
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }



}
