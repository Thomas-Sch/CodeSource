/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;

/// <summary>
/// Reposition the game object with the defined Tag on the opposite side of the board. 
/// </summary>
public class Reposition : MonoBehaviour {

	/// <summary>
	/// When the gameobject leaves the area, it's repositionned on the other side of the area.
	/// </summary>
	/// <param name="other">The leaving gameobject</param>
	void OnTriggerExit(Collider other) {
		Transform t = other.transform;
		if(other.CompareTag(Parameters.OrganismTag)) {
			t.position = new Vector3(t.position.x * -1,t.position.y,t.position.z * -1);
		}
	}
}
