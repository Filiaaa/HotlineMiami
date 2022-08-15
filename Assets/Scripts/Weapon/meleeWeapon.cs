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
			if(transform.parent.GetComponent<PlayerMover>() != null)
            {
				transform.parent.GetComponent<PlayerMover>().canThrow = false;

			}
            gameObject.SetActive(true);
			GetComponent<Animator>().SetBool("Attack", true);
			gameObject.transform.localPosition = Vector3.zero;
			transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
/*			gameObject.SetActive(true);*/
			attackSound.pitch = Random.Range (0.9f, 1.1f);
    		attackSound.Play ();
			attack = true;
/*			transform.parent.GetComponent <SpriteRenderer> ().enabled = false;*/
			attackCol.enabled = true;

			StartCoroutine (waitForEnablingCol());
		}
		return true;
		
	}

	IEnumerator waitForEnablingCol()
	{

		yield return new WaitForSeconds (attackTime);
		gameObject.SetActive(false);
		/*		GetComponent<Animator>().SetBool("Attack", false);
				gameObject.transform.localPosition = offsetInHands;*/
		attackCol.enabled = false;
		transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		/*		transform.parent.GetComponent <SpriteRenderer> ().enabled = true;*/
		attack = false;
		if (transform.parent.GetComponent<PlayerMover>() != null)
		{
			transform.parent.GetComponent<PlayerMover>().canThrow = true;

		}

		/*		gameObject.SetActive(false);*/
	}
}
