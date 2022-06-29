using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public GameObject currentWeapon, knife, killedPlayer, restartPanel;
	public float movingSpeed;
	public bool canThrow = true;
	public AudioSource steps;

	float hor, vert, angle;
	Rigidbody2D rb;
	bool attacked = true;
	
	private void Start () {
		currentWeapon = knife;
		rb = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate () {
		if (currentWeapon == null) {
			currentWeapon = knife;
			GetComponent <Animator> ().SetBool ("withKnife", true);
			knife.SetActive (true);
		}
		if (Input.GetMouseButton (0)) {
			currentWeapon.SetActive (true);
			attacked = currentWeapon.GetComponent <Weapon> ().Attack();
			if (!attacked) {
				currentWeapon = null;
			}
		} 

		if (!Input.GetMouseButton (0)) {
			GetComponent <Animator> ().SetBool ("Attack", false);
			canThrow = true;
			if (currentWeapon != null && currentWeapon != knife)
				currentWeapon.GetComponent <Animator> ().SetBool ("Attack", false);
			// currentWeapon.SetActive (false);
		}

// 		else if (!Input.GetMouseButton (0)) {
// /*            GetComponent<Animator>().SetBool("Attack", false);*/
//       if (currentWeapon != knife) {
// 				currentWeapon.SetActive (false);
// /*				currentWeapon.GetComponent<Animator>().SetBool("Attack", false);*/
			// }
		// }


		vert = Input.GetAxis ("Vertical");
		hor = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(hor, vert) * movingSpeed;
    if (rb.velocity == Vector2.zero) {
			GetComponent <Animator> ().SetBool("walk", false);
			steps.Stop();
		}
		else  {
			GetComponent <Animator> ().SetBool("walk", true);
			if (!steps.isPlaying) {
				steps.pitch = Random.Range (0.9f, 1.1f);
				steps.PlayDelayed (0.15f);
			}
		}

		var mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition); //положение мыши из экранных в мировые координаты
		angle = Vector2.Angle (Vector2.up, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
/*        if (Vector2.Distance(mousePosition, transform.position) > 0.6f)
		{*/
		transform.eulerAngles = new Vector3 (0f, 0f, transform.position.x < mousePosition.x ? -angle : angle);//немного магии на последок
/*        }*/

	}

	private void OnTriggerStay2D (Collider2D collision) {
		if (collision.tag == "Weapon" && Input.GetMouseButtonDown (1) && canThrow) {
			if (currentWeapon != knife) {
				currentWeapon.GetComponent <Weapon> ().Throw();
			} 
			GetComponent <Animator> ().SetBool ("withKnife", false);
			currentWeapon = collision.gameObject;
			currentWeapon.GetComponent <Weapon> ().Take (transform);
			// currentWeapon.SetActive (false);
		}

	}

	GameObject curRoom;
	
	void OnTriggerEnter2D (Collider2D collision) {
		
		if (collision.tag == "Room") {
			
			curRoom = collision.gameObject;
			curRoom.GetComponent <Manager> ().enemysAgr();

		} else if (collision.tag == "EnemyAttack") {
			KillPlayer ();
		}
	}

	void OnTriggerExit2D (Collider2D collision) {
		
		if (collision.tag == "Room") {

			curRoom.GetComponent <Manager> ().enemysAgr();
			curRoom = null;

		}
	}

	void KillPlayer () {
		Instantiate (killedPlayer, transform.position, transform.rotation);
		restartPanel.SetActive (true);
        if (curRoom != null) curRoom.GetComponent<Manager>().enemysAgr();
		Destroy (gameObject);
	}

}
