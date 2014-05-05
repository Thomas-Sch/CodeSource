using UnityEngine;

/// <summary>
/// Reposition the game object with the defined Tag on the opposite side of the board. 
/// </summary>
public class Reposition : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Transform t = other.transform;
		if(other.CompareTag(Simulation.OrganismTag)) {
			t.position = new Vector3(t.position.x * -1,t.position.y,t.position.z * -1);
		}
	}
}
