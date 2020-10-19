using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    private float leftXMax = -2.4f, rightXMax = 2.4f;
    public float moveSpeed = 2f;
    private bool dir = true;
    private PlayerScripts player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScripts>();
    }


    void FixedUpdate()
    {
        if (player)
        {
            if (player.score > 30)
            {
                Move();
            }
        }

        Move();
    }


    void Move()
    {

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (transform.position.x <= leftXMax || transform.position.x >= rightXMax)
        {
            transform.Rotate(Vector3.up * 180);
        }
    }
}
