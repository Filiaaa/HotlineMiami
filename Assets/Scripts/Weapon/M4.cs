using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Weapon
{
	public GameObject bullet;
	public int force;
	public float attackTime;
	bool canAttack = true;

	public override void Attack()
	{

		if (canAttack)
		{
			var bullet_ = Instantiate(bullet, transform.position, Quaternion.identity, null);
			bullet_.GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
			canAttack = false;
			StartCoroutine(TimeBeforeAttack());
		}

	}


	IEnumerator TimeBeforeAttack()
	{

		yield return new WaitForSeconds(attackTime);
		/*        GetComponent<Animator>().SetBool("isAttacking", false);*/
		canAttack = true;

	}
}
