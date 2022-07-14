using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Vector3 offsetInHands;
	public Quaternion offsetRotationInHands;
	public float soundRadius;


	public Sprite spriteInHands;

	public virtual bool Attack () {
		return true;
	}


	public void Take (Transform parent) {
		transform.parent = parent;
		transform.localPosition = offsetInHands;
		transform.localRotation = offsetRotationInHands;
		GetComponent <BoxCollider2D> ().enabled = false;
		GetComponent <Animator> ().SetBool ("onFloor", false);
		GetComponent<Animator>().SetBool("InHands", true);
		/*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
	}

	public virtual void Throw () {
		transform.parent = null;
		GetComponent <Animator> ().SetBool ("onFloor", true);
		GetComponent<Animator>().SetBool("InHands", true);
		GetComponent <BoxCollider2D> ().enabled = true;
	}
}
