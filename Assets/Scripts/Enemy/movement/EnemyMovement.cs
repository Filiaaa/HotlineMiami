using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform [] wayPoints;
	public Sprite deathBody;
	public GameObject playerObj, enter, curWeapon, killedEnemy;
	public float minDistance = 0.5f, movingSpeed = 1, deltaDictance = 0.3f;
	public bool agred = false;

	private int wayPointNumber;
	private Transform enemy, player;
	private bool isReturning = false;

	void Start () {
		enemy = GetComponent <Transform> (); 
		player = playerObj.GetComponent <Transform> ();
		curWeapon.GetComponent<Animator>().SetBool("onFloor", false);
		curWeapon.GetComponent<Animator>().SetBool("Enemys", true);
	}

	private float angle;
	 
	void FixedUpdate () {
        if (player == null)
        {
			agred = false;

		}
		if (!isReturning) {
			if (!agred) {
				enemy.Translate (Vector2.up * movingSpeed);
				curWeapon.GetComponent<Animator>().SetBool("Attack", false);

				angle = Vector2.Angle (Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);

			} else{

				if (Vector2.Distance(enemy.position, player.position) - deltaDictance > minDistance) {
					enemy.Translate (Vector2.up * movingSpeed);
				} else if (Vector2.Distance(enemy.position, player.position) + deltaDictance < minDistance) {
					enemy.Translate (Vector2.down * movingSpeed);
				}

				angle = Vector2.Angle (Vector2.up, player.position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < player.position.x ? -angle : angle);

				if (curWeapon != null) {
					curWeapon.GetComponent <Weapon> ().Attack ();
				}

			}
		} else {
			angle = Vector2.Angle (Vector2.up, enter.GetComponent <Transform> ().position - enemy.position);
			enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < enter.GetComponent <Transform> ().position.x ? -angle : angle);

			enemy.Translate (Vector2.up * movingSpeed);

		}
	}


	public void ArriveToPoint () {
		wayPointNumber++;
		wayPointNumber %= wayPoints.Length;
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Room") {
			isReturning = true;
		}
	}

	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.tag == "Room") {
			isReturning = false;
		} else if (collision.tag == "PlayerAttack") {
			KillEnemy ();
		}
	}

	void KillEnemy () {
		curWeapon.GetComponent<Animator>().SetBool("Enemys", false);
		if(curWeapon.GetComponent<FireWeapon>() != null) curWeapon.GetComponent<FireWeapon>().bulletsInHolder = curWeapon.GetComponent<FireWeapon>().bulletsNormalInHolder;

		curWeapon.GetComponent <Weapon> ().Throw();
	 	GameObject deathBody_ = Instantiate (killedEnemy, transform.position, transform.rotation);
		deathBody_.GetComponent<SpriteRenderer>().sprite = deathBody;
		Destroy (gameObject);
	}

}
