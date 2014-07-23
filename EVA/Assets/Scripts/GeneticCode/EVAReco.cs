using System;
using System.Collections.Generic;
using GeneticLibrary.Collections;
using GeneticLibrary.Mutations;
using GeneticLibrary.Mutations.GeneticModifiers;
using Tools;
using Wrappers;

namespace GeneticLibrary.Recombination
{
    /// <summary>
    /// The recombinaison handler for the simulation.
    /// </summary>
    class EVAReco : IRecombination
    {
		private static EVAReco Singleton;

        private const int minChild = 1;
        private const int maxChild = 5;

		public static IRecombination getInstance() {
			if(Singleton == null) {
				Singleton = new EVAReco();
			}
			return Singleton;
		}

        public RecombinationOutput Recombine(Genotype a, Genotype b)
        {
            IList<Genotype> offsprings = new List<Genotype>();

            int nbrChild = UnityEngine.Random.Range(minChild, maxChild);

            for (var i = 0; i < nbrChild; ++i)
            {
				Genotype g = new Genotype();
                Extension e;

                // 50% chance to inherit the base extension from one of the parents.
                if (Probability.Test(0.5))
                    e = a.Root;
                else
                    e = b.Root;

                g.Root = e.LocalClone();

                recombinaison(g.Root, a.Root, b.Root);

                // Mutations
                if (Probability.Test(0.3))
                {
                    Mutation mutation = new Mutation();
                    mutation.AddGeneticModifier(new Blur(new Set(new[] { "root" }, Set.Mode.Blacklist), new Set(new[] { "scale" }), 0.1f));
                    //mutation.AddGeneticModifier(new Addition(new Set(new[] { "Motor" }, Set.Mode.Whitelist), new Set(new[] { "scale" }), 2));
                    g.Mutate(mutation);
                }
                

                offsprings.Add(g);
            }

            return new RecombinationOutput(offsprings);
        }

        /// <summary>
        /// Recursivly modify the genotype by mixing 50/50 the parents genetic material.
        /// </summary>
        /// <param name="child">The current extension of the child</param>
        /// <param name="parentA">The current extension of the parent a</param>
        /// <param name="parentB">The current extension of the parent b</param>
        private void recombinaison(Extension child, Extension parentA, Extension parentB)
        {
            // The structure recombination is done only for the first level.
            IEnumerator<Extension> t1 = (IEnumerator<Extension>)parentA.GetEnumerator();
            IEnumerator<Extension> t2 = (IEnumerator<Extension>)parentB.GetEnumerator();

            while (t1.MoveNext() && t2.MoveNext())
            {
                Extension e;
                if (Probability.Test(0.5))
                    e = (Extension)t1.Current.LocalClone();
                else
                    e = (Extension)t2.Current.LocalClone();

                child.AddExtension(e);

                recombinaison(e, t1.Current, t2.Current);
            }
        }
    }
}
