/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>

using UnityEngine;
using GeneticLibrary;
using GeneticLibrary.Wrappers;
using GeneticLibrary.Mutations;
using GeneticLibrary.Collections;
using GeneticLibrary.Interfaces;
using Simulation.Handling;
using GeneticCode.Mutations.GeneticModifiers;

/// <summary>
/// Behaviour of a type D organism.
/// </summary>
public class D : Organism {

	/// <summary>
	/// Overriding lifeduration parameters.
	/// </summary>
	protected override void ExtendGenotype() {
		base.ExtendGenotype();
		Genotype.Root.SetGeneticData("lifeduration", new WFloat(1000));
	}

    protected override void Initialisation()
    {
        base.Initialisation();


        Extension Arm = Genotype.Root.getChild(0);
        Arm.Tag = "Arm";

        Extension Motor = Arm.getChild(0);
        Extension Segments = Arm.getChild(1);

        Motor.Tag = "Motor";
        Segments.Tag = "Segments";

        Segments.getChild(0).Tag = "First";
        Segments.getChild(1).Tag = "Second";
    }

	#region implemented abstract members of Organism

	protected override IMutation PreSpawnMutation ()
	{
		Mutation result = new Mutation();
		result.AddGeneticModifier(new Blur(Set.ALL, new Set(new [] {"scale"}), 0.2F));

        // It won't have much effect since the joints force to be aligned.
        result.AddGeneticModifier(new Blur(Set.ALL, new Set(new[] { "rotation" }), 10));
		result.AddGeneticModifier(new SingleBlur(new Set(new [] {"root"}),new Set(new [] {"lifeduration"}), 500));
		return result;
	}

	public override GameObject Prefab ()
	{
		return SimHandler.D;
	}

	#endregion
}
