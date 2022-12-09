using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : Weapon
{
	public Collider2D attackCol;

	public float attackTime;
	public AudioSource attackSound;

	public bool attack = false;
	
	public override bool Attack() {
		
		if (!attack) {

            gameObject.SetActive(true);
			if (transform.parent != null && transform.parent.GetComponent<PlayerMover>() != null)
			{
				transform.parent.GetComponent<PlayerMover>().canThrow = false;
				GetComponent<Animator>().SetBool("Attack", true);

			}
			gameObject.transform.localPosition = Vector3.zero;
			transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
/*			attackSound.pitch = Random.Range (0.9f, 1.1f);
    		attackSound.Play ()*/;
			attack = true;
			attackCol.enabled = true;

			StartCoroutine (waitForEnablingCol());
		}
		return true;
		
	}

	IEnumerator waitForEnablingCol()
	{

		yield return new WaitForSeconds (attackTime);
		if (transform.parent != null && transform.parent.GetComponent<PlayerMover>() != null)
		{
			attack = false;
			gameObject.SetActive(false);
			attackCol.enabled = false;
			transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;

			transform.parent.GetComponent<PlayerMover>().canThrow = true;

		}

	}
}
