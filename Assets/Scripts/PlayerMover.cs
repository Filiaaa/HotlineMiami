using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public GameObject currentWeapon, knife, killedPlayer, restartPanel;
	public float movingSpeed;
	float hor, vert, angle;
	
	Rigidbody2D rb;
	bool attacked = true;
	
	private void Start () {
		currentWeapon = knife;
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		if (currentWeapon == null) {
			currentWeapon = knife;
			knife.SetActive (true);
		}
		if (Input.GetMouseButton(0)) {
			attacked = currentWeapon.GetComponent <Weapon> ().Attack();
			if (!attacked) {
				currentWeapon = null;
			}
		}


		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(hor, vert) * movingSpeed;

		var mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //��������� ���� �� �������� � ������� ����������
		angle = Vector2.Angle(Vector2.up, mousePosition - transform.position);//���� ����� �������� �� ������� � ���� � ���� �
/*        if (Vector2.Distance(mousePosition, transform.position) > 0.6f)
		{*/
			transform.eulerAngles = new Vector3 (0f, 0f, transform.position.x < mousePosition.x ? -angle : angle);//������� ����� �� ��������
/*        }*/

	}

	private void OnTriggerStay2D (Collider2D collision) {
		if (collision.tag == "Weapon" && Input.GetMouseButtonDown (1)/* && !currentWeapon.GetComponent<Animator>().GetBool("isAttacking")*/) {
			if (collision.gameObject.GetComponent <BoxCollider2D> ().enabled) {
				if (currentWeapon != knife) {
					currentWeapon.GetComponent <Weapon> ().Throw();
				} else {
					knife.SetActive (false);
				}
			}
			currentWeapon = collision.gameObject;
			currentWeapon.transform.parent = transform;
			currentWeapon.GetComponent <Weapon> ().Take();
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
		restartPanel.SetActive (true);
		Destroy (gameObject);
	}

}
