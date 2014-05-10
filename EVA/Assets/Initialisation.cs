using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour {

	private static GameObject A;
	private static GameObject B;

	private static readonly float SpawnHeight = 2f;
	private float SemiSideLength;

	public GameObject Terrain;

	void Awake() {
		A = Resources.Load<GameObject>(Organism.Path + "Template A");
		B = Resources.Load<GameObject>(Organism.Path + "Template B");
	}

	// Use this for initialization
	void Start () {
		if(Terrain == null) {
			Debug.LogError("Terrain is not set !");
		}

		SemiSideLength = transform.localScale.x * Terrain.transform.localScale.x * 4;
		for(var i = 0; i < Simulation.InitialPopulation; ++i) {
			Spawn (A);
		}

//		for(var i = 0; i < Simulation.InitialPopulation; ++i) {
//			Spawn (B);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void Spawn(GameObject prefab) {	
		Quaternion rotation = Random.rotation;
		Vector3 r = rotation.eulerAngles;
		r.x = 0;
		r.z = 0;
		rotation.eulerAngles = r;

		GameObject instance = Instantiate(prefab, GetRandomPosition(), rotation) as GameObject;
	}

	private Vector3 GetRandomPosition() {
		Vector3 position = new Vector3();
		position.y = SpawnHeight;
		position.x = Random.Range(-SemiSideLength, SemiSideLength);
		position.z = Random.Range(-SemiSideLength, SemiSideLength);
		return position;
	}
}
