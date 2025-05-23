using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public float agringDistanse;
	public Transform [] wayPoints;
	public BoxCollider2D weaponCol;
	public Sprite deathBody;
	public GameObject playerObj, curWeapon, killedEnemy;
	public float minDistance = 0.5f, movingSpeed = 0.01f, deltaDictance = 0.3f;
	public bool agred = false, walk = true;
	public AudioSource stepsSound;
	public Transform [] enters;
	public int heals = 1;
	[SerializeField] private LayerMask obstacleLayerMask;

	private int wayPointNumber;
	private Transform player, enter, enemy;
	private bool isReturning = false;
	RaycastHit2D[] results;

	void Start () {
		enemy = GetComponent <Transform> (); 
		player = playerObj.GetComponent <Transform> ();
		if (curWeapon != null) {
			curWeapon.GetComponent <Animator> ().SetBool ("onFloor", false); 
			curWeapon.GetComponent <Animator> ().SetBool ("InHands", true);
		}
	}

	private float angle;

/*    private void OnDrawGizmos()
    {
		var dir = playerObj.transform.position - transform.position;
		RaycastHit hit;
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, dir);
		if (Physics.Raycast(playerObj.transform.position, dir.normalized, out hit))
		{
			print('S');
			if (hit.transform == player) {print("HitPlayer");  }
			else
			{
				print("Thing");
				Gizmos.DrawLine(playerObj.transform.position, hit.transform.position);
				Gizmos.color = Color.yellow;
			}
			// ��������
		}
	}*/

    void FixedUpdate () {
/*		if (Physics2D.Linecast(transform.position, player.position, obstacleLayerMask))
		{
			print("ThereAreCols");
		}*/
		//agring or disagring
		/*		var dir = player.position - transform.position;
				RaycastHit hit;
				if (Physics.Raycast(transform.position, dir.normalized, out hit))
				{
					print(hit.transform.gameObject.name);
				}
		*/
		if (player != null && !Physics2D.Linecast(transform.position, player.position, obstacleLayerMask) && Vector2.Angle (transform.up, player.transform.position - transform.position) < 30 && Vector2.Distance (enemy.position, player.position) <= agringDistanse/* && player.GetComponent <PlayerMover> ().curRoom == transform.parent.gameObject*/) {
			movingSpeed = 0.3f;
			agred = true;
		} 
		else if (player != null && /*player.GetComponent <PlayerMover> ().curRoom != transform.parent.gameObject*/ Physics2D.Linecast(transform.position, player.position, obstacleLayerMask))
		{
			movingSpeed = 0.1f;
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

				curWeapon.GetComponent <Animator> ().SetBool ("Attack", false);
				
				if (weaponCol != null) weaponCol.enabled = false;
				if (curWeapon != null) curWeapon.GetComponent <Animator> ().SetBool ("Attack", false);

				angle = Vector2.Angle (Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);

			} else if (player != null) {
				if (Vector2.Distance (enemy.position, player.position) - deltaDictance > minDistance) {
					enemy.Translate (Vector2.up * movingSpeed);

					curWeapon.GetComponent <Animator> ().SetBool ("Attack", false);
					
					if (weaponCol != null) 
						weaponCol.enabled = false;

				} else if (Vector2.Distance  (enemy.position, player.position) + deltaDictance < minDistance) {
					enemy.Translate (Vector2.down * movingSpeed);

					curWeapon.GetComponent <Animator> ().SetBool ("Attack", false);

					if (weaponCol != null) 
						weaponCol.enabled = false;

				} else {
					walk = false;

					curWeapon.GetComponent <Animator> ().SetBool ("Attack", true);

					if (weaponCol != null) { 
						weaponCol.enabled = true;
					}
				}

				angle = Vector2.Angle (Vector2.up, player.position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < player.position.x ? -angle : angle);

				if (curWeapon != null) {
					curWeapon.GetComponent <Weapon> ().Attack ();
				}
				
			}
		} else {

			curWeapon.GetComponent <Animator> ().SetBool ("Attack", false);
			
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
			enter = FindEnter ();
		}
	}

	Transform FindEnter () {
		int minNumber = 0;
		for (int i = 1; i < enters.Length; i++) {
			if (Vector2.Distance (enters[minNumber].position, enemy.position) > Vector2.Distance (enters[i].position, enemy.position))
				minNumber = i;
		}
		return enters[minNumber];
	}

	void OnTriggerStay2D (Collider2D collision) {
		if (collision.tag == "Room") {
			if (collision.gameObject == transform.parent.gameObject) {
				isReturning = false;
			} else {
				isReturning = true;
				enter = FindEnter();
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "PlayerAttack")
		{
			KillEnemy();
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "PlayerAttack")
		{
			if (collision.gameObject.GetComponent<Bullet>()) {Destroy(collision.gameObject);}
			KillEnemy();
		}
	}

    void KillEnemy () {
/*		if (!isImmortal)*/
			heals-= playerObj.GetComponent<PlayerMover>().damage;
/*		isImmortal = true;*/
/*		StartCoroutine(ImmortalFrames());*/
		if (heals <= 0) {
			if (weaponCol != null) weaponCol.enabled = false;
			if (curWeapon != null && curWeapon.GetComponent <FireWeapon> () != null) curWeapon.GetComponent <FireWeapon> ().bulletsInHolder = curWeapon.GetComponent <FireWeapon> ().bulletsNormalInHolder;

			if (curWeapon != null) curWeapon.GetComponent <Weapon> ().Throw();
		 	GameObject deathBody_ = Instantiate (killedEnemy, transform.position, transform.rotation);
			deathBody_.GetComponent<SpriteRenderer>().sprite = deathBody;
			Destroy (gameObject);
		}
	}
/*
	IEnumerator ImmortalFrames(){
		yield return new WaitForSeconds(0.6f);
		isImmortal = false;
	}
*/
}
