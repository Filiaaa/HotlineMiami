using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public GameObject currentWeapon, kick;
	public float movingSpeed;
	float hor, vert, angle;
	Rigidbody2D rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			currentWeapon.GetComponent<Weapon>().Attack();
		}
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(hor * movingSpeed, vert * movingSpeed);
		var mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
		angle = Vector2.Angle(Vector2.up, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
/*        if (Vector2.Distance(mousePosition, transform.position) > 0.6f)
		{*/
			transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < mousePosition.x ? -angle : angle);//немного магии на последок
/*        }*/

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Weapon" && Input.GetKeyDown(KeyCode.E)/* && !currentWeapon.GetComponent<Animator>().GetBool("isAttacking")*/)
		{
			print('d');
			if (currentWeapon != kick && collision.gameObject.GetComponent<BoxCollider2D>().enabled)
			{
				currentWeapon.transform.parent = null;
			}
			currentWeapon = collision.gameObject;
			collision.gameObject.transform.parent = transform;
			collision.gameObject.transform.localRotation = Quaternion.identity;
			collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			collision.gameObject.GetComponent<Weapon>().Take();
		}

	}


	bool inRoom = false;	
	GameObject curRoom;
	
	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log (collision.gameObject);
		if (collision.tag == "Room") {
			if (inRoom) {
				inRoom = false;
			} else {
				inRoom = true;
				curRoom = collision.gameObject;
				curRoom.GetComponent <Manager>().enemysAgro();
			}
		}
	}
}
