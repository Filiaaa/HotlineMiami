using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
	public BoxCollider2D colInPlayersHands;
	public GameObject curBullet, playerBullet;
	public float attackTime, beforeNextBulletTime;
	public float minVarience;
	public float maxVarience;
	public int force, bulletsInQueue, bulletsInHolder, bulletsNormalInHolder;
	public	bool canAttack = true;
	public AudioSource attackSound;

	Vector3 navigation;

	public override bool Attack () {
		if (transform.parent.tag == "Player")
		{
			colInPlayersHands.enabled = true;
		}
		if (canAttack && bulletsInHolder > 0) {
			GetComponent<Animator>().SetBool("Attack", true);
			attackSound.pitch = Random.Range (0.95f, 1.05f);
			attackSound.Play();
/*			GetComponent<SpriteRenderer>().enabled = true;*/
			/*if (transform.parent.tag == "Player")
			{
				transform.parent.GetComponent<Animator>().SetBool("Attack", true);
			}*/

			bulletsInHolder--;
			StartCoroutine (QueueAttack ());
			/*			bullet.GetComponent<Bullet>().navigation = transform.right;*/

			StartCoroutine (TimeBeforeAttack ());
			canAttack = false;
		} 
		if (bulletsInHolder <= 0) {
			if (transform.parent.tag == "Player")
			{
				transform.parent.GetComponent<PlayerMover>().canThrow = true;
			}
			Throw ();
			GetComponent <BoxCollider2D> ().enabled = false;
			return false;
		}

		return true;

	}

	IEnumerator QueueAttack () {
		for (int bulletNumber = 0; bulletNumber < bulletsInQueue; bulletNumber++) {
			Vector3 instPos = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
			var bullet_ = Instantiate (curBullet, instPos, transform.rotation, null);
			navigation = new Vector3 (transform.up.x + Random.Range (minVarience, maxVarience), transform.up.y + Random.Range (minVarience, maxVarience), transform.up.z);
			bullet_.GetComponent <Rigidbody2D> ().AddForce (navigation * force * PlayerMover.bulletSpeedBuff/*, ForceMode2D.Impulse*/);
			yield return new WaitForSeconds (beforeNextBulletTime);
		}
	}

	IEnumerator TimeBeforeAttack () {

		yield return new WaitForSeconds (attackTime);
		if (transform.parent != null && transform.parent.GetComponent<PlayerMover>() != null)
		{
			transform.parent.GetComponent<PlayerMover>().canThrow = true;
		}
		// transform.parent.GetComponent <Animator> ().SetBool ("Attack", false);
		// GetComponent <Animator> ().SetBool ("Attack", false);
		canAttack = true;

	}

	public override void Throw () {
		GetComponent<FireWeapon>().colInPlayersHands.enabled = false;
		GetComponent<Animator>().SetBool("InHands", false);
		GetComponent <BoxCollider2D> ().enabled = true;
		GetComponent <Animator> ().SetBool ("onFloor", true);
		GetComponent <Animator> ().SetBool ("Attack", false);
		curBullet = playerBullet;
		transform.parent = null;
	} 

}
