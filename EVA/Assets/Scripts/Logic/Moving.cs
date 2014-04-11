using UnityEngine;
using System.Collections;
using System;
using GeneticCode.Tools;

public class Moving : MonoBehaviour {

	public float strength = 0.5F;
	public float smooth = 0.5F;
	private Quaternion newRotation;

	// Use this for initialization
	void Start () {
		newRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newPos = new Vector3(strength * transform.forward.x, 0, strength * transform.forward.z);
		transform.Translate(newPos, Space.World);

		if(Probability.Test(0.01)) {
			newRotation = UnityEngine.Random.rotation;
			Vector3 v = newRotation.eulerAngles;
			v.x = 0;
			v.z = 0;
			newRotation.eulerAngles = v;
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * smooth);
	}
}
