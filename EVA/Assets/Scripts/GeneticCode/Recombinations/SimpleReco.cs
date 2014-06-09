using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Mutations;
using GeneticLibrary.Mutations.GeneticModifiers;
using Tools;

namespace GeneticLibrary.Recombination
{
    /// <summary>
    /// This recombination operator uses a structure recombination. It does not modifies the genes but only the structure.
    /// </summary>
    class SimpleReco : IRecombination
    {
		private static SimpleReco Singleton;

		private const int nbrChild = 1;

		public static IRecombination getInstance() {
			if(Singleton == null) {
				Singleton = new SimpleReco();
			}
			return Singleton;
		}

        public RecombinationOutput Recombine(Genotype a, Genotype b)
        {
            IList<Genotype> offsprings = new List<Genotype>();
            var _50p = new Probability(0.5);
            var _1p = new Probability(0.01);

            for (var i = 0; i < nbrChild; ++i)
            {
				Genotype g = new Genotype();
                Extension e;

                // 50% chance to inherit the base extension from one of the parents.
                if (_50p.Test())
                    e = a.Root;
                else
                    e = b.Root;

                g.Root = e.LocalClone();

                // The structure recombination is done only for the first level.
                IEnumerator<Extension> t1 = (IEnumerator <Extension>) a.Root.GetEnumerator();
                IEnumerator<Extension> t2 = (IEnumerator <Extension>) b.Root.GetEnumerator();

                while (t1.MoveNext() && t2.MoveNext()){
                    if (_50p.Test())
                        g.Root.AddExtension((Extension)t1.Current.DeepClone());
                    else
                        g.Root.AddExtension((Extension)t2.Current.DeepClone());
                } 
                
                // Rare mutation that extends plate size.
                if (_1p.Test())
                {
                    Mutation m = new Mutation();
                    //m.addGeneticModifier(new Multiplication(new Set(new[] { "scale" }), new Set(new[] { "Plate" }), 1.5));
                    g.Mutate(m);
                }

                offsprings.Add(g);
            }

            return new RecombinationOutput(offsprings);
        }
    }
}
