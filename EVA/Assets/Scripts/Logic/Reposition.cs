using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reposition : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Transform t = other.transform;
		if(other.CompareTag("Reposable")) {
			t.position = new Vector3(t.position.x * -1,t.position.y,t.position.z * -1);
		}
	}
}
