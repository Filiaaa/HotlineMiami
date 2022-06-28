using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : Weapon
{
	public Collider2D attackCol;
	public float attackTime;
	bool attack = false;
	
	public override bool Attack() {
    if (!attack) {
			attack = true;
			transform.parent.GetComponent <SpriteRenderer> ().enabled = false;
			attackCol.enabled = true;
      StartCoroutine (waitForEnablingCol());
    }
    return true;
		
	}

	IEnumerator waitForEnablingCol()
	{
		yield return new WaitForSeconds(attackTime);
		attackCol.enabled = false;
		transform.parent.GetComponent<SpriteRenderer>().enabled = true;
		attack = false;
		gameObject.SetActive(false);
	}
}
