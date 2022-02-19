using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {

    }
}