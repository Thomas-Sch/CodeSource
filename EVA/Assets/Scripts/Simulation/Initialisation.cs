/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using UnityEditor;

/// <summary>
/// Class d'initialisation pour la simulation.
/// </summary>
public class Initialisation : MonoBehaviour {

	public static GameObject A;
	public static GameObject B;
	public static GameObject D;

	public static string Path = "Prefabs/";

	private static readonly float SpawnHeight = 2f;
	private float SemiSideLength;

	public GameObject Terrain;

	void Awake() {
		A = Resources.Load<GameObject>(Path + "Template A");
		B = Resources.Load<GameObject>(Path + "Template B");
		D = Resources.Load<GameObject>(Path + "Template D");
		Time.timeScale = 1;
	}

	public static void SimulationEndStatistics(){
		Debug.Log("Time elapsed: " + Time.realtimeSinceStartup);
		Debug.Log("Number of frames: " + Time.frameCount);
		Debug.Log("Number of organisms: " + Organism.NumberOfOrganisms);
	}

	public static void StopSimulation() {
		EditorApplication.isPlaying = false;
		Application.Quit();
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
	}

	void Update() {
		if (Input.GetKey (KeyCode.O)) {
			Time.timeScale = 0.5f;
		}
		if (Input.GetKey (KeyCode.R)) {
			Time.timeScale = 1f;
		}
		if (Input.GetKey (KeyCode.P)) {
			Time.timeScale = 2f;
		}
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
