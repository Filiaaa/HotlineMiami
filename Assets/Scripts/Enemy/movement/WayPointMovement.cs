using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour {

	public int wayPointNumber;
	public Transform [] wayPoints;
	public float navigation;

	private Transform enemy;
	private float navigationTime = 0;

	void Start () {
		enemy = GetComponent <Transform> (); 
	}
	 
	void Update () {
		navigationTime += Time.deltaTime;
		if (navigationTime > navigation) {
			enemy.position = Vector2.MoveTowards (enemy.position, wayPoints[wayPointNumber].position, navigationTime);
			navigationTime = 0;
		}
	}

	// void OnTriggerEnter2D (Collider2D collision) {
	// 	if (collision.tag == "WayPoint") {
	// 		wayPointNumber++;
	// 		wayPointNumber %= wayPoints.Length;
	// 	}
	// }

	public void ArriveToPoint () {
		wayPointNumber++;
		wayPointNumber %= wayPoints.Length;
	}

}
