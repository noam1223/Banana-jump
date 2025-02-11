﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform target;

    private bool followPlayer;

    public float minYTreshold = -2.6f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {

        if(target.position.y < (transform.position.y - minYTreshold))
        {
            followPlayer = false;
        }

        if (target.position.y > transform.position.y)
        {
            followPlayer = true;
        }

        if (followPlayer)
        {
            Vector3 temp = transform.position;
            temp.y = target.position.y;
            transform.position = temp;
        }
    }
}
