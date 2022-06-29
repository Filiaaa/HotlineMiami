using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
	public GameObject curBullet, playerBullet;
	public float attackTime, beforeNextBulletTime;
	public float minVarience;
	public float maxVarience;
	public int force, bulletsInQueue, bulletsInHolder, bulletsNormalInHolder;
	public	bool canAttack = true;
	public AudioSource attackSound;

	Vector3 navigation;

	public override bool Attack () {
		if (canAttack && bulletsInHolder > 0) {
			attackSound.pitch = Random.Range (0.95f, 1.05f);
			attackSound.Play();

			transform.parent.GetComponent <Animator> ().SetBool ("Attack", true);
			GetComponent <Animator> ().SetBool ("Attack", true);

			bulletsInHolder--;
			StartCoroutine (QueueAttack ());
			/*			bullet.GetComponent<Bullet>().navigation = transform.right;*/

			StartCoroutine (TimeBeforeAttack ());
			canAttack = false;

		} 
		if (bulletsInHolder <= 0) {
			transform.parent.GetComponent <PlayerMover> ().canThrow = true;
			Throw ();
			GetComponent <BoxCollider2D> ().enabled = false;
			return false;
		}

		return true;

	}

	IEnumerator QueueAttack () {
		for (int bulletNumber = 0; bulletNumber < bulletsInQueue; bulletNumber++) {
			var bullet_ = Instantiate (curBullet, transform.position, Quaternion.identity, null);
			navigation = new Vector3 (transform.up.x + Random.Range (minVarience, maxVarience), transform.up.y + Random.Range (minVarience, maxVarience), transform.up.z);
			bullet_.GetComponent <Rigidbody2D> ().AddForce (navigation * force/*, ForceMode2D.Impulse*/);
			yield return new WaitForSeconds (beforeNextBulletTime);
		}
	}

	IEnumerator TimeBeforeAttack () {

		yield return new WaitForSeconds (attackTime);
		if (transform.parent != null && transform.parent.GetComponent<PlayerMover>() != null)
			transform.parent.GetComponent <PlayerMover> ().canThrow = true;
		// transform.parent.GetComponent <Animator> ().SetBool ("Attack", false);
		// GetComponent <Animator> ().SetBool ("Attack", false);
		canAttack = true;

	}

	public override void Throw () {
		GetComponent <BoxCollider2D> ().enabled = true;
		GetComponent <Animator> ().SetBool ("onFloor", true);
		GetComponent <Animator> ().SetBool ("Attack", false);
		curBullet = playerBullet;
		transform.parent = null;
	} 

}
