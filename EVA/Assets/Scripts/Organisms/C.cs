/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
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
public class C : Organism {

    /// <summary>
    /// Overriding lifeduration and speed parameters.
    /// </summary>
    new private void ExtendGenotype() {
        base.ExtendGenotype();
        Genotype.Root.SetGeneticData("lifeduration", new WFloat(500));
        Genotype.Root.SetGeneticData("speed", new WFloat(0.2F));
    }

    #region implemented abstract members of Organism

    public override GameObject Prefab ()
    {
         return SimHandler.C;
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
