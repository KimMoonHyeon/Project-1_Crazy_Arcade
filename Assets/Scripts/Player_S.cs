using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    private Vector3 move_Vec;
    private int bullet_count;
    public List<GameObject> Water_List = new List<GameObject>();

    // 물풍선 배열을 만드는데, 크기는 일단 water_length만큼 고정으로 만들긴 해야하고, 

    public GameObject Water;
    public GameObject Bullet;

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
            //Debug.Log(Water_List.Count);
            if (Water_List.Count < water_max)
            {
                // 1,2,3
                GameObject new_Water = Instantiate(Water, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);
                Water_List.Add(new_Water);
                new_Water.gameObject.GetComponent<Collider>().isTrigger = true;
                StartCoroutine("Water_Boom");
                //터질 때, 3d 큐브로 한 번 재현해보기 Trigger 이용해서 충돌판정
            }
            
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


        if (this.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform Gun_pos = this.gameObject.transform.GetChild(1).gameObject.transform;
                Rigidbody bullet_rigid = Instantiate(Bullet, new Vector3(Gun_pos.position.x, Gun_pos.position.y, Gun_pos.position.z), Quaternion.identity).GetComponent<Rigidbody>();
                bullet_rigid.velocity = transform.forward * 5;
                bullet_count--;
                if (bullet_count <= 0)
                {
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }

    }

    void Water_push(GameObject Water)
    {
        Water.transform.position -= new Vector3(-1, 0, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }
    private void OnCollisionEnter(Collision collision)
    {   //if를 넣어 밀기 아이템을 먹었을 때만 해당 if를 true로 만들어 밀기 가능하도록 만들기
        if(collision.gameObject.tag == "Water")
        {
            Rigidbody water_rigid = collision.gameObject.GetComponent<Rigidbody>();
            water_rigid.velocity = move_Vec * 25;
            //그러니까 워터끼리의 충돌을 없애면 되오나, 통과는 되지 않게 하기.
        }
    }


    //코루틴 함수
    IEnumerator Water_Boom()
    {
        yield return new WaitForSeconds(10f);
        Destroy(Water_List[0]);
        Water_List.RemoveAt(0);
    }
}
