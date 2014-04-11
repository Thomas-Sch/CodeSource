using UnityEngine;
using System.Collections;
using GeneticCode;
using GeneticCode.Organisms;
using GeneticCode.Wrappers;
using GeneticCode.BodyParts;
using GeneticCode.Mutations.Modifiers;
using GeneticCode.Mutations;

public class A : MonoBehaviour {
	public Genotype g;

	// Use this for initialization
	void Start () {
		g = new Genotype();
		g.RootElement = new Square(new GeneticData());
		DeductGenotype(transform, g.RootElement);

		Mutation init = new Mutation();
		init.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		g.Mutate(init);

		ModifyPhenotype(g);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Deducts the genotype.
	/// </summary>
	/// <param name="t">T. The current transform component examined.</param>
	/// <param name="e">E. The last extension treated (will serve as the parent for this call)</param>
	void DeductGenotype(Transform t, Extension e) {
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
	void ModifyPhenotype(Genotype g) {
		ModifyElement(transform, g.RootElement);
	}

	void ModifyElement(Transform t, Extension e) {
		t.localPosition = ((WVector3)e.GeneticData.Get("position")).Value;
		t.localRotation = Quaternion.Euler(((WVector3)e.GeneticData.Get("rotation")).Value);
		t.localScale = ((WVector3)e.GeneticData.Get("scale")).Value;

		IEnumerator transforms = t.GetEnumerator();
		IEnumerator extensions = e.GetEnumerator();

		while(transforms.MoveNext() && extensions.MoveNext()) {
			ModifyElement((Transform)transforms.Current, (Extension)extensions.Current);
		}


	}
}
