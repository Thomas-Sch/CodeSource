using UnityEngine;
using System.Collections;
using GeneticLibrary;
using Wrappers;
using GeneticLibrary.BodyParts;

public class Organism : MonoBehaviour {

	protected static string Path = "Prefabs/";

	public BasicActionState BasicState {get; set;}
	public float Smooth = 0.5F;
	public PhenotypeData phenotypeData; // Need renommage maybe. Confusion entre le phenotype des extensions et des données.
	public Genotype Genotype {get; set;}
	public enum BasicActionState {Birth, Movement, Reproduction, Death}

	// Use this for initialization<
	public void Start () {
	
	}

	public void Awake() {
		Genotype = new Genotype();
		Genotype.RootElement = new Square(new GeneticData());

		phenotypeData = new PhenotypeData();

		DeductGenotype(transform, Genotype.RootElement);
		ExtendGenotype(Genotype.RootElement);

		BasicState = BasicActionState.Birth;
	}
	
	// Update is called once per frame
	public void Update () {
		Debug.Log("No behaviour has been set.");
	}

	#region Genetic modifications

	/// <summary>
	/// Deducts the genotype.
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
	public void ModifyPhenotype(Genotype g) {
		ModifyElement(transform, g.RootElement);
		
		phenotypeData.LifeExpectancy = (int)((WFloat)g.RootElement.GeneticData.Get("lifeexpectancy")).Value;
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
		element.SetGeneticData("lifeexpectancy", new WFloat(500));
		element.SetGeneticData("speed", new WFloat(0.2F));
	}
	#endregion

	#region Death of the organism
	public void Kill() {
		Destroy(gameObject);
	}
	#endregion
}
