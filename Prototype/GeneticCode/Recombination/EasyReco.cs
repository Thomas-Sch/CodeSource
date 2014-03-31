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
    class EasyReco : RecombinationHandler
    {
        private const int nbrChild = 1;

        public static RecombinationHandler getInstance()
        {
            if (instance == null)
            {
                instance = new EasyReco();
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
                Organism o = a.createEmptyOrganism();
                Extension e;

                // 50% chance to inherit the base extension from one of the parents.
                if (_50p.test())
                    e = a.genotype.rootElement;
                else
                    e = b.genotype.rootElement;

                o.genotype.rootElement = (BodyPart) e.copyExtension();

                // The structure recombination is done only for the first level.
                var nExtensions = Math.Min(a.genotype.rootElement.getNumberOfChildExtensions(), b.genotype.rootElement.getNumberOfChildExtensions());

                IEnumerator<Extension> t1 = (IEnumerator<Extension>) a.genotype.rootElement.getChildsEnumerator();
                IEnumerator<Extension> t2 = (IEnumerator <Extension>) b.genotype.rootElement.getChildsEnumerator();

                while (t1.MoveNext() && t2.MoveNext()){
                    if (_50p.test())
                        o.genotype.rootElement.addExtension(t1.Current.copyAll());
                    else
                        o.genotype.rootElement.addExtension(t2.Current.copyAll());
                } 
                
                // Rare mutation that extends plate size.
                if (_1p.test())
                {
                    Mutation m = new Mutation();
                    m.addGeneticModifier(new Multiplication(new Set(new[] { "scale" }), new Set(new[] { "Plate" }), 1.5));
                    o.mutate(m);
                }

                offsprings.Add(o);
            }

            return offsprings.ToArray<Organism>();
        }
    }
}
