using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string playerAnimParametre;
	public int soundRadius = 0;
	public Vector3 offsetInHands;
	public Quaternion offsetRotationInHands;
	public Sprite spriteInHands;


	public virtual bool Attack () {
		return true;
	}


	public void Take (Transform parent) {

		transform.parent = parent;
        if (transform.parent.GetComponent<PlayerMover>() != null)
        {
			transform.parent.GetComponent<Animator>().SetBool(playerAnimParametre, true);
			if(spriteInHands != null)
            {
				transform.parent.GetComponent<PlayerMover>().movingPart.sprite = spriteInHands;
			}
		
		}
		transform.localPosition = offsetInHands;
		transform.localRotation = offsetRotationInHands;
		GetComponent <BoxCollider2D> ().enabled = false;
		GetComponent <Animator> ().SetBool ("onFloor", false);
		GetComponent<Animator>().SetBool("InHands", true);
		/*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
	}

	public virtual void Throw () {
		gameObject.SetActive(true);
		if (transform.parent.GetComponent<PlayerMover>() != null)
		{
			transform.parent.GetComponent<Animator>().SetBool(playerAnimParametre, false);
		}
		transform.parent = null;
		GetComponent <Animator> ().SetBool ("onFloor", true);
		GetComponent<Animator>().SetBool("InHands", true);
		GetComponent <BoxCollider2D> ().enabled = true;
	}
}
