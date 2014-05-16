/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using GeneticLibrary;
using GeneticLibrary.Wrappers;
using GeneticLibrary.Mutations.GeneticModifiers;
using GeneticLibrary.Mutations;
using GeneticLibrary.Collections;
using GeneticLibrary.Interfaces;

/// <summary>
/// Behaviour of a type D organism.
/// </summary>
public class D : Organism {

	/// <summary>
	/// Rajoute des informations au génotype.
	/// </summary>
	new private void ExtendGenotype() {
		base.ExtendGenotype();
		Genotype.RootElement.SetGeneticData("lifeduration", new WFloat(500));
		Genotype.RootElement.SetGeneticData("speed", new WFloat(0.2F));
	}

	#region implemented abstract members of Organism

	public override GameObject Prefab ()
	{
		return Initialisation.D;
	}

	protected override IMutation PreSpawnMutation ()
	{
		Mutation result = new Mutation();
		result.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.5F));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeduration"}), 100.0F));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}), new Set(new [] {"speed"}), 0.2F));
		return result;
	}

	#endregion
}
