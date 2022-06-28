using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
/*    public Vector3 navigation;
	public int speed;*/

	public float liveTime;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "wall")
		{
			Destroy(gameObject);
		}

	}

	void Awake () {
		StartCoroutine (TimeBeforeDestory ());
	}

	IEnumerator TimeBeforeDestory () {

		yield return new WaitForSeconds (liveTime);

		Destroy (gameObject);
	}

/*    private void Update()
	{
		transform.Translate(navigation * speed * Time.deltaTime);
	}*/
}
