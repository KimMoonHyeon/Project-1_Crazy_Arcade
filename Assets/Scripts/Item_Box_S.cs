using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Box_S : MonoBehaviour
{
    public List<GameObject> Item_List = new List<GameObject>();
    public GameObject Item_1;
    public GameObject Item_2;
    public GameObject Item_3;
    public GameObject Item_4;
    int Box_count;

    int item_random;
    int item_parent;
    void Start()
    {
        GameObject New_Item;

        Item_List.Add(Item_1);
        Item_List.Add(Item_2);
        Item_List.Add(Item_3);
        Item_List.Add(Item_4);

        Box_count = GameObject.Find("Item_Box").gameObject.transform.childCount;
        Debug.Log("Box_Count" + Box_count);
        for (int i = 0; i < Box_count / 2; i++)
        {
            item_parent = Random.RandomRange(0, Box_count);
            item_random = Random.RandomRange(0, Item_List.Count);
            Debug.Log(i + 1 + ":" + "(1):" + item_parent + "(2)" + item_random);
            if (GameObject.Find("Item_Box").gameObject.transform.GetChild(item_parent).gameObject.transform.childCount == 0)
            {

                New_Item = Instantiate(Item_List[item_random], GameObject.Find("Item_Box").gameObject.transform.GetChild(item_parent).gameObject.transform.position, Quaternion.identity);
                New_Item.transform.parent = GameObject.Find("Item_Box").gameObject.transform.GetChild(item_parent).gameObject.transform;
            }
            else if (GameObject.Find("Item_Box").gameObject.transform.GetChild(item_parent).gameObject.transform.childCount > 0)
            {
                Debug.Log("-");
                //i--;
            }

        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water_Particle")
        {

        }
    }
}
