using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Vector3 offsetInHands;
	public Quaternion offsetRotationInHands;

	public Sprite spriteInHands;

	public virtual bool Attack () {
		return true;
	}


	public void Take () {
		transform.localPosition = offsetInHands;
		transform.localRotation = offsetRotationInHands;
		GetComponent <BoxCollider2D> ().enabled = false;
		GetComponent<Animator>().SetBool("onFloor", false);
		/*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
	}

	public virtual void Throw () {
		transform.parent = null;
		GetComponent<Animator>().SetBool("onFloor", true);
		GetComponent <BoxCollider2D> ().enabled = true;
	}
}
