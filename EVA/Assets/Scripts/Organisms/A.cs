using UnityEngine;
using System.Collections;
using GeneticLibrary;
using GeneticLibrary.Wrappers;
using GeneticLibrary.BodyParts;
using GeneticLibrary.Mutations.GeneticModifiers;
using GeneticLibrary.Mutations;
using GeneticLibrary.Recombination;
using GeneticLibrary.Collections;
using Tools;

public class A : Organism {
	private static GameObject Prefab = Resources.Load<GameObject>(Path + "Template A");

	public int Age {get; set;}
	
	private Quaternion newRotation;

	private float seeingDistance = 10F;

	public new void Awake() {
		base.Awake();

		Age = 0;
		
		Mutation init = new Mutation();
		init.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeexpectancy"}), 100.0F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}), new Set(new [] {"speed"}), 0.2F));
		Genotype.Mutate(init);
		
		ModifyPhenotype(Genotype);
		
		// The organism start moving.
		BasicState = BasicActionState.Movement;
	}

	new void FixedUpdate() {
		if(BasicState == A.BasicActionState.Movement) {
			
			// On déplace la forme seulement si elle n'est pas en l'air.
			if(transform.position.y < 1.0F){
				Vector3 newPos = new Vector3(phenotypeData.Speed * transform.forward.x, 0, phenotypeData.Speed * transform.forward.z);
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
	
	// Update is called once per frame. Action
	new void Update () {
		// Death of the organism.
		if(BasicState != BasicActionState.Death && Age >= phenotypeData.LifeExpectancy) {
			BasicState = BasicActionState.Death;
			Invoke("Kill", 4F);
		}

		if(CanReproduce()) {
			RaycastHit hit;
			var rayDirection = transform.forward;
			Debug.DrawRay(transform.position, rayDirection * seeingDistance);
			Physics.Raycast(transform.position, rayDirection, out hit, seeingDistance);
			
			if(hit.collider != null && hit.collider.CompareTag("Organism")) {
				A other = hit.collider.gameObject.GetComponent<A>();
				if(other != null && other.CanReproduce()) {
					other.Reproduce(gameObject);
					Reproduce(other.gameObject);

					Genotype[] childrenGenotype = SimpleReco.getInstance().Recombine(Genotype,other.Genotype);
					foreach(Genotype child in childrenGenotype) {
						Vector3 position = new Vector3(transform.forward.x * -0.5f, transform.position.y, transform.forward.z * -0.5f);
//						prefab.g = child; DO NOT FORGET.
						GameObject childInstance = Instantiate(Prefab, position, transform.localRotation) as GameObject;
						childInstance.SetActive(true);
					}
				}
			}
		}
		Age++;
	}

	#region Reproduction
	public void Reproduce(GameObject other) {
		BasicState = BasicActionState.Reproduction;
		StartCoroutine(ReproductionApproach(other));

	}

	IEnumerator ReproductionApproach (GameObject target)
	{
		if(target.GetComponent<A>().BasicState != BasicActionState.Death) { // A placer dans la boucle =)...
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
		return BasicState != BasicActionState.Death && BasicState != BasicActionState.Birth && BasicState != BasicActionState.Reproduction;
	}
	#endregion
}
