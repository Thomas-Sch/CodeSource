using UnityEngine;
using System.Collections;
using GeneticLibrary;
using GeneticLibrary.Wrappers;
using GeneticLibrary.BodyParts;
using GeneticLibrary.Mutations.Modifiers;
using GeneticLibrary.Mutations;
using GeneticLibrary.Recombination;
using GeneticLibrary.Collections;

public class A : MonoBehaviour {
	public A prefab;
	public Genotype g {get; set;}
	public PhenotypeData phenotypeData; // Need renommage maybe. Confusion entre le phenotype des extensions et des données.
	public int Age {get; set;}

	public ActionState State {get; set;}

	public enum ActionState {Birth, Movement, Reproduction, Death}

	private float seeingDistance = 10F;

	void Awake() {
		Age = 0;
		State = ActionState.Birth;
		g = new Genotype();
		phenotypeData = new PhenotypeData();

		g.RootElement = new Square(new GeneticData());
		DeductGenotype(transform, g.RootElement);
		ExtendGenotype(g.RootElement);
		
		Mutation init = new Mutation();
		init.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeexpectancy"}), 100.0F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}), new Set(new [] {"speed"}), 0.2F));
		g.Mutate(init);
		
		ModifyPhenotype(g);
		
		// The organism start moving.
		State = ActionState.Movement;
	}

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame. Action
	void Update () {
		// Death of the organism.
		if(State != ActionState.Death && Age >= phenotypeData.LifeExpectancy) {
			State = ActionState.Death;
			Invoke("Kill", 4F);
		}

		if(CanReproduce()) {
			RaycastHit hit;
			var rayDirection = transform.forward;
			Debug.DrawRay(transform.position, rayDirection * seeingDistance);
			Physics.Raycast(transform.position, rayDirection, out hit, seeingDistance);
			
			if(hit.collider != null && hit.collider.CompareTag("Reposable")) {
				A other = hit.collider.gameObject.GetComponent<A>();
				if(other.CanReproduce()) {
					other.Reproduce(gameObject);
					Reproduce(other.gameObject);
					Genotype[] childrenGenotype = SimpleReco.getInstance().Recombine(g,other.g);
					foreach(Genotype child in childrenGenotype) {
						Vector3 position = new Vector3(transform.forward.x * -0.5f, transform.position.y, transform.forward.z * -0.5f);

						GameObject childInstance = Instantiate(prefab, position, transform.localRotation) as GameObject;
						while(childInstance == null)
							Debug.Log("No child instance");
					}
				}
			}
		}
		Age++;
	}

	#region Reproduction
	public void Reproduce(GameObject other) {
		State = ActionState.Reproduction;
		StartCoroutine(ReproductionApproach(other));

	}

	IEnumerator ReproductionApproach (GameObject target)
	{
		if(target.GetComponent<A>().State != ActionState.Death) { // A placer dans la boucle =)...
			while(Vector3.Distance(transform.position, target.transform.position) > 2F)
			{
				transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(transform.position - target.transform.position), 0.02F);
				transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.3F * Time.deltaTime);
				yield return null;
			}
			
			print("Reached the target.");
			
		} else {
			print("My partner died :(");
		}
	}
	
	
	public bool CanReproduce() {
		return State != ActionState.Death && State != ActionState.Birth && State != ActionState.Reproduction;
	}
	#endregion

	#region Genetic modifications

	void ExtendGenotype (Extension element)
	{
		element.Tag = "root";
		element.SetGeneticData("lifeexpectancy", new WFloat(500));
		element.SetGeneticData("speed", new WFloat(0.5F));
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

		phenotypeData.LifeExpectancy = (int)((WFloat)g.RootElement.GeneticData.Get("lifeexpectancy")).Value;
		phenotypeData.Speed = ((WFloat)g.RootElement.GeneticData.Get("speed")).Value;
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
	#endregion

	#region Death of the organism
	public void Kill() {
		Destroy(gameObject);
	}
	#endregion
}
