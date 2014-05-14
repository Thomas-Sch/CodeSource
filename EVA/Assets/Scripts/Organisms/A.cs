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
using States;
using GeneticLibrary.Interfaces;

public class A : Organism {

	/// <summary>
	/// Rajoute des informations au génotype.
	/// </summary>
	new protected void ExtendGenotype() {
		base.ExtendGenotype();
		Genotype.RootElement.SetGeneticData("lifeduration", new WFloat(700));
		Genotype.RootElement.SetGeneticData("speed", new WFloat(0.1F));
	}

	#region implemented abstract members of Organism

	protected override IMutation PreSpawnMutation ()
	{
		Mutation result = new Mutation();
		result.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeduration"}), 100.0F));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}), new Set(new [] {"speed"}), 0.1F));
		return result;
	}

	public override GameObject Prefab ()
	{
		return Initialisation.A;
	}

	#endregion
}
