using System;
using System.Collections.Generic;
using GeneticCode.Organisms;
using GeneticCode.Mutations;
using GeneticCode.Mutations.Modifiers;
using GeneticCode.Mutations.StructuralOperations;
using GeneticCode.Tools;
using GeneticCode.Recombination;
using GeneticCode.GeneticDataTemplates;
using System.Threading;
using GeneticCode.Interfaces;
using UnityEngine;

namespace GeneticCode
{
	class Simulation
	{
		/// <summary>
        /// CRAPL license.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Debug.Log("FIN - Deaths:" + death + " - Organisms left:" + organisms.Count);
            Debug.Log("Creating new organisms...");
            Mutation blur = new Mutation();
            blur.AddGeneticModifier(new Blur(new Set(new[] { "rotation", "position" }), Set.ALL, 0.1F));
            Organism a = new Organisms.Test(blur);
			Organism b = new Organisms.Test(blur);

            Debug.Log(a);
            Debug.Log(b);

            Debug.Log("Their child:");
            RecombinationHandler recombination = SimpleReco.getInstance();
            Organism child = recombination.Recombine(a, b)[0];
            Debug.Log(child);

            Debug.Log("Wild mutations appeared !");
            Mutation m = new Mutation();
            m.AddGeneticModifier(new Addition(new Set(new[] { "scale" }, Set.Mode.Blacklist), new Set(new[] { "bob" }), 50.50F));
            m.AddGeneticModifier(new Addition(new Set(new[] { "position", "scale" }), new Set(new[] { "bob" }), -50.50F));
            m.AddGeneticModifier(new Addition(Set.ALL, new Set(new[] { "Plate" }), 100));
            a.Mutate(m);

            Mutation m2 = new Mutation();
            m2.AddStructuralModifier(new RemoveExtension(new Set(new[] { "Plate a" })));
            b.Mutate(m2);

            Debug.Log(a);
            Debug.Log(b);
            Debug.Log(child);
        }
	}
}
