using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
	public GameObject bullet;
	public float attackTime, beforeNextBulletTime;
	public float minVarience;
	public float maxVarience;
	public int force, bulletsInQueue, bulletNumber;
	public	bool canAttack = true;
	Vector3 navigation;

	public override void Attack () {

		if (canAttack) {
			var bullet_ = Instantiate(bullet, transform.position, Quaternion.identity, null);
			navigation = new Vector3 (transform.right.x + Random.Range (minVarience, maxVarience), transform.right.y + Random.Range (minVarience, maxVarience), transform.right.z);
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
		}

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

}
