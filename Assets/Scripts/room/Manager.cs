using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject [] enemys;

	public void enemysAgr () {
		for (int i = 0; i < enemys.Length; i++) {
			if (enemys[i] != null) {
				enemys[i].GetComponent <EnemyMovement>().agred = !enemys[i].GetComponent<EnemyMovement>().agred;
			}
		}
	}

/*	public void enemysDisAgr () {
		for (int i = 0; i < enemys.Length; i++) {
			if (enemys[i] != null) {
				enemys[i].GetComponent <EnemyMovement> ().agred = false;
			}
		}
	}*/
}
