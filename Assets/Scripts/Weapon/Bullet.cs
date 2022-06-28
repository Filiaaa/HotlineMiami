using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
/*    public Vector3 navigation;
    public int speed;*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            Destroy(gameObject);
        }

    }

/*    private void Update()
    {
        transform.Translate(navigation * speed * Time.deltaTime);
    }*/
}
