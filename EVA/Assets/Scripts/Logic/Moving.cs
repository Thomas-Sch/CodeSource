using UnityEngine;
using System.Collections;
using System;
using GeneticLibrary.Tools;

public class Moving : MonoBehaviour {

	private A organism;

	private float Speed;
	public float Smooth = 0.5F;
	private Quaternion newRotation;

	// Use this for initialization
	void Start () {
		newRotation = transform.rotation;
		organism = gameObject.GetComponent<A>(); // A modifier !

		Speed = organism.phenotypeData.Speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(organism.State == A.ActionState.Movement) {

			// On déplace la forme seulement si elle n'est pas en l'air.
			if(transform.position.y < 1.0F){
				Vector3 newPos = new Vector3(Speed * transform.forward.x, 0, Speed * transform.forward.z);
				transform.Translate(newPos, Space.World);
			}

			if(Probability.Test(0.01)) {
				newRotation = UnityEngine.Random.rotation;
				Vector3 v = newRotation.eulerAngles;
				v.x = 0;
				v.z = 0;
				newRotation.eulerAngles = v;
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * Smooth);
		}
	}
}
