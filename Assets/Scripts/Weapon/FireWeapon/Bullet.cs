using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*    public Vector3 navigation;
        public int speed;*/
    /*
        public float liveTime;*/
    public int ricochetCount/*, force*/;
    /*    public Transform normal;
        Vector3 result;
        float velocity;*/
    


    private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "wall")
		{

            if (ricochetCount == 0)
            {
                Destroy(gameObject);
            }
            ricochetCount--;
            print("A");
            /*            velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
                        ricochetCount--;
                        result = Vector3.Reflect(GetComponent<Rigidbody2D>().velocity,   collision.transform.right) * 10;
                        transform.LookAt(new Vector3(result.x, 0, 0));
                        GetComponent<Rigidbody2D>().velocity = result.normalized * velocity;*/
            /*            Debug.Break();*/
            /*            GetComponent<Rigidbody2D>().AddForce(transform.up * force*//*, ForceMode2D.Impulse*//*);*/
            /*Debug.DrawRay(collision.transform.position, result * 10, Color.blue);
            Debug.Break();*/
        }

	}


/*
    private void Update()
    {
*//*        Debug.DrawRay(transform.position, transform.up * 10, Color.red);*//*
        result.position = Vector3.Reflect(transform.up, normal.right) * 10;
        Debug.DrawRay(normal.position, normal.right * 10, Color.green);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red);
        Debug.DrawRay(normal.position, result.position * 10, Color.blue);
    }*/

    /*    void Awake()
        {
            StartCoroutine(TimeBeforeDestory());
        }

        IEnumerator TimeBeforeDestory()
        {

            yield return new WaitForSeconds(liveTime);

            Destroy(gameObject);
        }*/

    /*    private void Update()
        {
            transform.Translate(navigation * speed * Time.deltaTime);
        }*/
}
