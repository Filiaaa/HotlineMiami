using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public int wayPointNumber;
	public Transform [] wayPoints;
	public float navigation;
	public GameObject playerObj;

	private Transform enemy;
	private Transform player;
	private float navigationTime = 0;
	private bool agred = false;

	void Start () {
		enemy = GetComponent <Transform> (); 
		player = playerObj.GetComponent <Transform> ();
	}

	private float angle;
	 
	void Update () {
			navigationTime += Time.deltaTime;
		if (!agred) {
			if (navigationTime > navigation) {
				enemy.position = Vector2.MoveTowards (enemy.position, wayPoints[wayPointNumber].position, navigationTime);

				angle = Vector2.Angle (Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);

				navigationTime = 0;
			}
		} else {
			if (navigationTime > navigation) {
				enemy.position = Vector2.MoveTowards(enemy.position, player.position, navigationTime);

				angle = Vector2.Angle (Vector2.up, player.position - enemy.position);
				enemy.eulerAngles = new Vector3 (0, 0, enemy.position.x < player.position.x ? -angle : angle);

				navigationTime = 0;
			}
		}
	}


	public void ArriveToPoint () {
		wayPointNumber++;
		wayPointNumber %= wayPoints.Length;
	}

	public void Agro () {
		agred = true;
		transform.parent = null;
	}

}
