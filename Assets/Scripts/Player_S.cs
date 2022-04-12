using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;
    private int water_name;

    private int bullet_count; //총알 장전 스킬
    private int water_max; //물풍선 개수 늘리기 스킬
    private int barrier_time;
    private float speed; // 스피드업 스킬
    private float water_boom_size; //물풍선 길이 증가 스킬


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
        rigid.velocity = Vector3.zero; //Water와 충돌시 밀림 방지, 가속도 = 0;

        float m_x = Input.GetAxisRaw("Horizontal");
        float m_z = Input.GetAxisRaw("Vertical");
        
        move_Vec = new Vector3(m_x, 0, m_z);
        //plyaer 이동
        this.transform.position += move_Vec.normalized * speed * Time.deltaTime;

        //player 회전
        if(m_z != 0 || m_x != 0)
            this.transform.rotation = Quaternion.LookRotation(new Vector3(m_x,0,m_z));

    }

    void Plyaer_Skiil()
    {
        
       
        //space -> 물풍선 생성
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
        else if (Input.GetKeyDown("1")) //스피드 증가
        {
            speed += 3f;
        }
        else if (Input.GetKeyDown("2")) //밀기
        {

        }
        else if (Input.GetKeyDown("3")) //물풍선 개수 추가
        {
            water_max++;
        }
        else if (Input.GetKeyDown("4")) //총 발사해서 물풍선 터뜨리기
        {
            bullet_count += 3; //불렛 카운트는 아이템을 먹었을 때로 이동하기, 먹은 상태에서 4번을 눌렀을 때 총이 나와야하니까, Input.GetKeyDown(4) && bullet_count >0 조건 변경
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (Input.GetKeyDown("5")) //물풍선 길이 늘리기, 늘리기는 되오나, 해당 물풍선을 설치하고 아이템을 먹으면,아이템 먹기 전 길이까지 같이 커지는 버그
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
            Debug.Log("으악!");

        }
    }

    private void OnCollisionEnter(Collision collision)
    {   //if를 넣어 밀기 아이템을 먹었을 때만 해당 if를 true로 만들어 밀기 가능하도록 만들기
        if (collision.gameObject.tag == "Water")
        {
            Rigidbody water_rigid = collision.gameObject.GetComponent<Rigidbody>();
            water_rigid.velocity = move_Vec * 25;
            //그러니까 워터끼리의 충돌을 없애면 되오나, 통과는 되지 않게 하기.
        }
    }

    //코루틴 함수
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

    IEnumerator Barrier_TIme() //중복해서 실행할 수 있게 만들어야함, 
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        while (barrier_time > 0)
        {
            Debug.Log("방어막 사용시간:" + barrier_time);
            yield return new WaitForSeconds(1f);
            barrier_time--;
        }
        if (barrier_time <= 0)
        {
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }



}
