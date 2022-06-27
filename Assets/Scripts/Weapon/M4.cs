using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Weapon
{
	public GameObject bullet;
	public int force;

	public override void Attack()
	{
		var bullet_ =  Instantiate(bullet, null);
		bullet_.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
	}
}
