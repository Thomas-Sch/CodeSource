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

public class D : Organism {

	private Quaternion newRotation;

	public new void Awake() {
		base.Awake();

		Age = 0;
		
		Mutation init = new Mutation();
		init.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeduration"}), 100.0F));
		init.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}), new Set(new [] {"speed"}), 0.2F));
		Genotype.Mutate(init);
		
		ModifyPhenotype(Genotype);
	}

	#region implemented abstract members of Organism

	public override GameObject Prefab ()
	{
		return Initialisation.D;
	}

	#endregion
}