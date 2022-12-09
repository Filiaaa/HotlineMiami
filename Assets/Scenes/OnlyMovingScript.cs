using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyMovingScript : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    float hor, vert;

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hor, vert) * 20;

    }
}
