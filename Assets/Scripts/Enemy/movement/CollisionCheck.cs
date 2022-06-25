using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "WayPoint") {
			transform.parent.GetComponent <WayPointMovement>().ArriveToPoint();
		}
	}

}
