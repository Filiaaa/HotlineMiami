using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string playerAnimParametre;
	public int soundRadius = 0;
	public Vector3 offsetInHands;
	public Quaternion offsetRotationInHands;
	public Sprite spriteInHands, pickUpSprite, defaultSprite, spriteInTrig;
	public SpriteRenderer sr;
	public float timeToTake;
	public Animator animator;

	private void Start()
    {
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
    }


    public virtual bool Attack () {
		return true;
	}



    public void Take (Transform parent) {
		animator.enabled = true;
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
		animator.SetBool ("onFloor", false);
		animator.SetBool("InHands", true);
		/*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
	}

	public virtual void Throw () {
		gameObject.SetActive(true);
		animator.enabled = false;
		if (transform.parent != null && transform.parent.GetComponent<PlayerMover>() != null)
		{
			transform.parent.GetComponent<Animator>().SetBool(playerAnimParametre, false);
		}

		transform.parent = null;
		if(GetComponent<meleeWeapon>() != null)
        {
			GetComponent<meleeWeapon>().attackCol.enabled = false;
			GetComponent<meleeWeapon>().attackCol.tag = "PlayerAttack";

		}
		GetComponent <BoxCollider2D> ().enabled = true;
	}
}
