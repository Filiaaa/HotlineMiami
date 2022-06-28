using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public GameObject currentWeapon, kick, killedPlayer;
	public float movingSpeed;
	float hor, vert, angle;
	Rigidbody2D rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{

		if (Input.GetMouseButton(0))
		{
			currentWeapon.GetComponent<Weapon>().Attack();
		}
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(hor, vert) * movingSpeed;

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
			if (currentWeapon != kick && collision.gameObject.GetComponent<BoxCollider2D>().enabled)
			{
				currentWeapon.GetComponent <Weapon> ().Throw();
			}
			currentWeapon = collision.gameObject;
			currentWeapon.transform.parent = transform;
			currentWeapon.GetComponent<BoxCollider2D>().enabled = false;
			currentWeapon.GetComponent<Weapon>().Take();
		}

	}

	GameObject curRoom;
	
	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.tag == "Room") {
			
			curRoom = collision.gameObject;
			curRoom.GetComponent <Manager>().enemysAgr();

		} else if (collision.tag == "EnemyAttack") {
			KillPlayer ();
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		
		if (collision.tag == "Room") {

			curRoom.GetComponent <Manager>().enemysDisAgr();
			curRoom = null;

		}
	}

	void KillPlayer () {
		Instantiate (killedPlayer, transform.position, transform.rotation);

		Destroy (gameObject);
	}

}
