using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
	public GameObject bullet;
	public float attackTime;
	public float minVarience;
	public float maxVarience;
	public int force;
	public	bool canAttack = true;
	Vector3 navigation;


	public override void Attack()
	{

		if (canAttack)
		{
			var bullet_ = Instantiate(bullet, transform.position, Quaternion.identity, null);
			navigation = new Vector3(transform.right.x + Random.RandomRange(minVarience, maxVarience), transform.right.y + Random.RandomRange(minVarience, maxVarience), transform.right.z);
			bullet_.GetComponent<Rigidbody2D>().AddForce(navigation * force/*, ForceMode2D.Impulse*/);
			/*			bullet.GetComponent<Bullet>().navigation = transform.right;*/
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
