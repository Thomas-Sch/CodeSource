using UnityEngine;

/// <summary>
/// Reposition the game object with the defined Tag on the opposite side of the board. 
/// </summary>
public class Reposition : MonoBehaviour {

	private static string Tag = "Organism";

	void OnTriggerExit(Collider other) {
		Transform t = other.transform;
		if(other.CompareTag(Tag)) {
			t.position = new Vector3(t.position.x * -1,t.position.y,t.position.z * -1);
		}
	}
}
