using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Mutations;
using GeneticCode.Mutations.Modifiers;
using GeneticCode.Organisms;
using GeneticCode.Tools;

namespace GeneticCode.Recombination
{
    /// <summary>
    /// This recombination operator uses a structure recombination. It does not modifies the genes but only the structure.
    /// </summary>
    class SimpleReco : RecombinationHandler
    {
        private const int nbrChild = 1;

        public static RecombinationHandler getInstance()
        {
            if (instance == null)
            {
                instance = new SimpleReco();
            }
            return instance;
        }

        public override Organism[] recombine(Organism a, Organism b)
        {
            IList<Organism> offsprings = new List<Organism>();
            var _50p = new Probability(0.5);
            var _1p = new Probability(0.01);

            for (var i = 0; i < nbrChild; ++i)
            {
                Organism o = a.createEmpty();
                Extension e;

                // 50% chance to inherit the base extension from one of the parents.
                if (_50p.test())
                    e = a.genotype.getRootElement();
                else
                    e = b.genotype.getRootElement();

                o.genotype.setRootElement(e.localClone());

                // The structure recombination is done only for the first level.
                var nExtensions = Math.Min(a.genotype.getRootElement().getNumberOfChildExtensions(), b.genotype.getRootElement().getNumberOfChildExtensions());

                IEnumerator<Extension> t1 = (IEnumerator <Extension>) a.genotype.getRootElement().getChildsEnumerator();
                IEnumerator<Extension> t2 = (IEnumerator <Extension>) b.genotype.getRootElement().getChildsEnumerator();

                while (t1.MoveNext() && t2.MoveNext()){
                    if (_50p.test())
                        o.genotype.getRootElement().addExtension((Extension)t1.Current.deepClone());
                    else
                        o.genotype.getRootElement().addExtension((Extension)t2.Current.deepClone());
                } 
                
                // Rare mutation that extends plate size.
                if (_1p.test())
                {
                    Mutation m = new Mutation();
                    //m.addGeneticModifier(new Multiplication(new Set(new[] { "scale" }), new Set(new[] { "Plate" }), 1.5));
                    o.mutate(m);
                }

                offsprings.Add(o);
            }

            return offsprings.ToArray<Organism>();
        }
    }
}
