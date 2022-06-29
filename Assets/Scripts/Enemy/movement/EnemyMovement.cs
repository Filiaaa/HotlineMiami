using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public float agringDistanse;
	public Transform [] wayPoints;
	public BoxCollider2D weaponCol;
	public Sprite deathBody;
	public GameObject playerObj, enter, curWeapon, killedEnemy;
	public float minDistance = 0.5f, movingSpeed = 1, deltaDictance = 0.3f;
	public bool agred = false, walk = true;
	public AudioSource stepsSound;

	private int wayPointNumber;
	private Transform enemy, player;
	private bool isReturning = false;

	void Start () {
		enemy = GetComponent <Transform> (); 
		player = playerObj.GetComponent <Transform> ();
		if (curWeapon != null)
		{
			curWeapon.GetComponent<Animator>().SetBool("onFloor", false); 
			curWeapon.GetComponent<Animator>().SetBool("Enemys", true);
		}
	}

	private float angle;
	 
	void FixedUpdate () {

		//agring or disagring

        if (player != null && Vector2.Angle(transform.up, player.transform.position - transform.position) < 100 && Vector2.Distance(enemy.position, player.position) <= agringDistanse && player.GetComponent<PlayerMover>().curRoom == transform.parent.gameObject)
        {
            agred = true;
        }
		else if(player != null && player.GetComponent<PlayerMover>().curRoom != transform.parent.gameObject)
        {
			agred = false;
        }


        //////////////////////////////////////////////////////////////////////////////////
        if (walk && !stepsSound.isPlaying) {
			stepsSound.pitch = Random.Range (0.9f, 1.1f);
			stepsSound.Play();
		} else if (!walk)
			stepsSound.Stop();
		walk = true;
		if (!isReturning) {
			if (!agred) {
				enemy.Translate (Vector2.up * movingSpeed);
				GetComponent<Animator>().SetBool("Attack", false);
				if(weaponCol != null) weaponCol.enabled = false;
				if (curWeapon != null) curWeapon.GetComponent<Animator>().SetBool("Attack", false);

				angle = Vector2.Angle (Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);

			} 
			else if(player != null){

				if (player != null && Vector2.Distance (enemy.position, player.position) - deltaDictance > minDistance) {
					enemy.Translate (Vector2.up * movingSpeed); 
					GetComponent<Animator>().SetBool("Attack", false);
					if (weaponCol != null) weaponCol.enabled = false;
				}
				else
				{
					walk = false; GetComponent<Animator>().SetBool("Attack", true);
					if (weaponCol != null) { weaponCol.enabled = true; }
				}
				/*else if (Vector2.Distance  (enemy.position, player.position) + deltaDictance < minDistance) {
					enemy.Translate (Vector2.down * movingSpeed); 
					GetComponent<Animator>().SetBool("Attack", false);
					if (weaponCol != null) weaponCol.enabled = false;*/
				/*}*/

				angle = Vector2.Angle (Vector2.up, player.position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < player.position.x ? -angle : angle);

				if (curWeapon != null) {
					curWeapon.GetComponent <Weapon> ().Attack ();
				}
				
			}
		} else {
			GetComponent<Animator>().SetBool("Attack", false);
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
		if (weaponCol != null) weaponCol.enabled = false;
		if (curWeapon != null)
		{
			curWeapon.GetComponent<Animator>().SetBool("Enemys", false);
			curWeapon.GetComponent<Animator>().SetBool("Attack", false);
		}
		if(curWeapon != null && curWeapon.GetComponent<FireWeapon>() != null) curWeapon.GetComponent<FireWeapon>().bulletsInHolder = curWeapon.GetComponent<FireWeapon>().bulletsNormalInHolder;

		if (curWeapon != null) curWeapon.GetComponent <Weapon> ().Throw();
	 	GameObject deathBody_ = Instantiate (killedEnemy, transform.position, transform.rotation);
		deathBody_.GetComponent<SpriteRenderer>().sprite = deathBody;
		Destroy (gameObject);
	}

}
