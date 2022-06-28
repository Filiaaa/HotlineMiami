using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform player;
/*
	float k = 2.0f;
	float maxOffset = 15;*/

	void Update()
	{
		if (player != null) {
			transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
		}
		/*
				var p = player.position;
				var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				p += Vector3.ClampMagnitude(mp - p, maxOffset) / k;
				p.z = transform.position.z;
		*//*        print(Vector3.Distance(mp, p));*//*
				transform.position = Vector3.Lerp(transform.position, p, 8f * Time.deltaTime);*/
	}
}
