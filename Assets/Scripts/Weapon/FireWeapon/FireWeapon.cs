using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
	public GameObject curBullet, playerBullet;
	public float attackTime, beforeNextBulletTime;
	public float minVarience;
	public float maxVarience;
	public int force, bulletsInQueue, bulletNumber, bulletsInHolder;
	public	bool canAttack = true;
	Vector3 navigation;

	public override bool Attack () {
		if (canAttack && bulletsInHolder > 0) {
			transform.parent.GetComponent<Animator>().SetBool("Attack", true);
			GetComponent<Animator>().SetBool("Attack", true);
			bulletsInHolder--;
			var bullet_ = Instantiate(curBullet, transform.position, Quaternion.identity, null);
			navigation = new Vector3 (transform.up.x + Random.Range (minVarience, maxVarience), transform.up.y + Random.Range (minVarience, maxVarience), transform.up.z);
			bullet_.GetComponent<Rigidbody2D>().AddForce(navigation * force/*, ForceMode2D.Impulse*/);
			/*			bullet.GetComponent<Bullet>().navigation = transform.right;*/

			if (bulletNumber < bulletsInQueue - 1) {
				StartCoroutine (TimeBeforeNextBullet ());
				bulletNumber++;
			} else {
				StartCoroutine(TimeBeforeAttack ());
				bulletNumber = 0;
				canAttack = false;
			}
		} else if (bulletsInHolder <= 0) {
			Throw ();
			transform.parent.GetComponent<Animator>().SetBool("withKnife", true);
			GetComponent <BoxCollider2D> ().enabled = false;
			return false;
		}

		return true;

	}


	IEnumerator TimeBeforeAttack () {

		yield return new WaitForSeconds(attackTime);
		/*        GetComponent<Animator>().SetBool("isAttacking", false);*/
		canAttack = true;

	}


	IEnumerator TimeBeforeNextBullet () {

		yield return new WaitForSeconds(beforeNextBulletTime);
		/*        GetComponent<Animator>().SetBool("isAttacking", false);*/

	}

	public override void Throw () {
		GetComponent <BoxCollider2D> ().enabled = true;
		curBullet = playerBullet;
		transform.parent = null;
	} 

}
