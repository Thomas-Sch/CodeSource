/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>

using UnityEngine;
using GeneticLibrary;
using GeneticLibrary.Wrappers;
using GeneticLibrary.Mutations.GeneticModifiers;
using GeneticLibrary.Mutations;
using GeneticLibrary.Collections;
using GeneticLibrary.Interfaces;
using Simulation.Handling;

/// <summary>
/// Behaviour of a type A organism.
/// </summary>
public class D : Organism {

	/// <summary>
	/// Overriding lifeduration and speed parameters.
	/// </summary>
	new protected void ExtendGenotype() {
		base.ExtendGenotype();
		Genotype.Root.SetGeneticData("lifeduration", new WFloat(700));
	}

	#region implemented abstract members of Organism

	protected override IMutation PreSpawnMutation ()
	{
		Mutation result = new Mutation();
		result.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.2F));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeduration"}), 100.0F));
		return result;
	}

	public override GameObject Prefab ()
	{
		return SimHandler.D;
	}

	#endregion
}
