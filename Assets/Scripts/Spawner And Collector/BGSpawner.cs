using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{

    private GameObject[] bgs;
    private float height;
    private float highestYPos;

    // Start is called before the first frame update
    void Awake()
    {
        bgs = GameObject.FindGameObjectsWithTag("BG");
    }

    void Start()
    {

        height = bgs[0].GetComponent<BoxCollider2D>().bounds.size.y;
        highestYPos = bgs[0].transform.position.y;

        for(int i = 1; bgs.Length > i; i++)
        {
            if(bgs[i].transform.position.y > highestYPos)
            {
                highestYPos = bgs[i].transform.position.y;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BG")
        {
            if(target.transform.position.y >= highestYPos)
            {
                Vector3 temp = target.transform.position;

                for(int i = 0; i < bgs.Length; i++)
                {
                    if (!bgs[i].activeInHierarchy)
                    {
                        temp.y += height;
                        bgs[i].transform.position = temp;
                        bgs[i].gameObject.SetActive(true);

                        highestYPos = temp.y; 
                    }
                }
            }
        }
    }
}
