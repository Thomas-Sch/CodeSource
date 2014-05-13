using UnityEngine;
using System.Collections;
using GeneticLibrary;
using Wrappers;
using GeneticLibrary.BodyParts;
using States;
using System;

public abstract class Organism : MonoBehaviour {

	private static int NumberOfOrganisms = 0;

	public int Name {get; private set;}

	public State State {get; set;}
	public PhenotypeData phenotypeData {get; set;} // Need renommage maybe. Confusion entre le phenotype des extensions et des données.
	public Genotype Genotype {get; set;}
	public int Age {get; set;}

	// StateChangingVariables //

	// Duration of this state based on the age of the organism.
	private static float PreAdultDuration = Simulation.PreAdultDuration;

	public void Awake() {
		Genotype = new Genotype();
		Genotype.RootElement = new Square(new GeneticData());

		phenotypeData = new PhenotypeData();

		DeductGenotype(transform, Genotype.RootElement);
		ExtendGenotype(Genotype.RootElement);

		Name = ++NumberOfOrganisms;
	}

	public void Start() {
		State = new Birth(this, BirthToPreAdult);
	}

	// Update is called once per frame
	public void Update () {
		try {
			State.Update();
		} catch (Exception e) {
			Debug.LogException(e);
		}
	}

	// Fixed Update is called once by physic engine
	public void FixedUpdate() {
		try {
			State.FixedUpdate();
		} catch (Exception e) {
			Debug.LogException(e);
		}

	}

	public abstract GameObject Prefab();

	/// <summary>
	/// Enable or disable the collider on the organism.
	/// </summary>
	/// <param name="b">If set to <c>true</c> b.</param>
	public void SetCollider(bool b) {
		collider.enabled = b;
		foreach(var child in GetComponentsInChildren<Collider>()) {
			child.enabled = b;
		}
	}

	public void Kill() {
		Destroy(gameObject);
	}

	#region State changing STAYS

	protected void BirthToPreAdult() {
		State = new PreAdult(this, PreAdultToAdult);
	}

	protected void PreAdultToAdult() {
		if((float)Age/phenotypeData.LifeDuration > PreAdultDuration)
			State = new Adult(this, AdultToDeath);
	}

	protected void AdultToDeath() {
		if(Age > phenotypeData.LifeDuration)
			State = new Death(this, null);
	}

	#endregion

	#region Genetic modifications
	/// <summary>
	/// Deducts the genotype. STAYS
	/// </summary>
	/// <param name="t">T. The current transform component examined.</param>
	/// <param name="e">E. The last extension treated (will serve as the parent for this call)</param>
	public void DeductGenotype(Transform t, Extension e) {
		e.GeneticData.Set("scale", new WVector3(t.localScale));
		e.GeneticData.Set("rotation", new WVector3(t.localRotation.eulerAngles));
		e.GeneticData.Set("position", new WVector3(t.localPosition));
		
		foreach(Transform child in t) {
			Extension eChild = new Square(new GeneticData());
			e.AddExtension(eChild);
			DeductGenotype(child, eChild);
		}
	}

	/// <summary>
	/// Modifies the phenotype accordingly to the genotype.
	/// </summary>
	/// <param name="g">The genotype used</param>
	public void ModifyPhenotype(Genotype g) { // DON'T STAYS
		ModifyElement(transform, g.RootElement);
		
		phenotypeData.LifeDuration = (int)((WFloat)g.RootElement.GeneticData.Get("lifeduration")).Value;
		phenotypeData.Speed = ((WFloat)g.RootElement.GeneticData.Get("speed")).Value;
	}
	
	private void ModifyElement(Transform t, Extension e) {
		t.localPosition = ((WVector3)e.GeneticData.Get("position")).Value;
		t.localRotation = Quaternion.Euler(((WVector3)e.GeneticData.Get("rotation")).Value);
		t.localScale = ((WVector3)e.GeneticData.Get("scale")).Value;
		
		IEnumerator transforms = t.GetEnumerator();
		IEnumerator extensions = e.GetEnumerator();
		
		while(transforms.MoveNext() && extensions.MoveNext()) {
			ModifyElement((Transform)transforms.Current, (Extension)extensions.Current);
		}
	}

	public void ExtendGenotype (Extension element)
	{
		element.Tag = "root";
		element.SetGeneticData("lifeduration", new WFloat(500));
		element.SetGeneticData("speed", new WFloat(0.2F));
	}
	#endregion

	override public string ToString() {
		return GetType() + Name.ToString();
	}
}
