using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour
{
    public Sprite spriteAfterHit;
	public ParticleSystem ps;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PlayerAttack")
		{
			GetComponent<SpriteRenderer>().sprite = spriteAfterHit;
			ps.gameObject.SetActive(true);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			if (collision.gameObject.GetComponent<Bullet>()) { Destroy(collision.gameObject); }
			GetComponent<SpriteRenderer>().sprite = spriteAfterHit;
			ps.gameObject.SetActive(true);
		}
	}
}
