using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : Weapon
{
	public Collider2D attackCol;
	public float attackTime;
	
	public override bool Attack()
	{
/*        if (!GetComponent<Animator>().GetBool("isAttacking"))
		{*/
		attackCol.enabled = true;
/*            GetComponent<Animator>().SetBool("isAttacking", true);*/
		StartCoroutine(waitForEnablingCol());
/*        }*/
		return true;
		
	}

	IEnumerator waitForEnablingCol()
	{
		yield return new WaitForSeconds(attackTime);
		attackCol.enabled = false;
/*        GetComponent<Animator>().SetBool("isAttacking", false);*/
	}
}
