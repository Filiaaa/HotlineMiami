using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Vector3 offsetInHands;
	public Quaternion offsetRotationInHands;

	public Sprite spriteInHands;

	public virtual void Attack()
	{

	}


	public void Take () {
		transform.localPosition = offsetInHands;
		transform.localRotation = offsetRotationInHands;
		/*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
	}

	public virtual void Throw () {
		transform.parent = null;
	}
}
