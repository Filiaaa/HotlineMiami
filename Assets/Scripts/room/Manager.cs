using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject [] enemys;

	public void enemysAgr () {
		for (int i = 0; i < enemys.Length; i++) {
			enemys[i].GetComponent <EnemyMovement>().Agr();
		}
	}

	public void enemysDisAgr () {
		for (int i = 0; i < enemys.Length; i++) {
			enemys[i].GetComponent <EnemyMovement> ().DisAgr();
		}
	}
}
