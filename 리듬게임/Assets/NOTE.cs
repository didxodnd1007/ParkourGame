﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTE : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * Speed*Time.deltaTime;
    }
}
