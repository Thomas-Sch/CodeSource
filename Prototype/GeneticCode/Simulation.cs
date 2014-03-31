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
using System.Collections.Concurrent;

namespace GeneticCode
{
	class Simulation
	{
        private static Random generator = new Random();
        private static List<Organism> organisms = new List<Organism>();
        private static int death = 0;

        /// <summary>
        /// CRAPL license.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            initialPopulation(organisms);

            while (organisms.Count > 1)
            {
                // Take two random organisms
                Organism[] parents = getTwoParents(organisms);

                // Mate them
                Organism[] children = EasyReco.getInstance().recombine(parents[0], parents[1]);

                // Profit
                for (var i = 0; i < children.Length; ++i)
                {
                    children[i].Died += OrganismDeath;
                    organisms.Add(children[i]);
                    children[i].start();
                }
                Console.WriteLine(organisms.Count);
            }

            foreach (Organism o in organisms.ToArray()) {
                o.thread.Join();
                Console.WriteLine(o.ToString());
            }

            Console.WriteLine("FIN - Deaths:" + death + " - Organisms left:" + organisms.Count);

            //Console.WriteLine ("Creating new organisms...");
            //Organism a = new Test(blur);
            //Organism b = new Test(blur);

            //Console.WriteLine(a);
            //Console.WriteLine(b);

            //Console.WriteLine("Their child:");
            //RecombinationHandler recombination = EasyReco.getInstance();
            //Organism child = recombination.recombine(a, b)[0];
            //Console.WriteLine(child);

            //Console.WriteLine("Wild mutations appeared !");
            //Mutation m = new Mutation();
            //m.addGeneticModifier(new Addition(new Set(new[] { "scale" }, Set.Mode.Blacklist), new Set(new [] {"bob"}), 50.50));
            //m.addGeneticModifier(new Addition(new Set(new[] { "position", "scale" }), new Set(new[] { "bob" }), -50.50));
            //m.addGeneticModifier(new Addition(Set.ALL, new Set(new[] { "Plate" }), 100));
            //a.mutate(m);

            //Mutation m2 = new Mutation();
            //m2.addStructuralModifier(new RemoveExtension(new Set(new [] {"Plate a"})));
            //b.mutate(m2);
        }

        // This will be called whenever the list changes.
        private static void OrganismDeath(object sender, EventArgs e) 
        {
            organisms.Remove((Organism)sender);
            death++;
        }

        private static Organism[] getTwoParents(List<Organism> organisms)
        {
            Organism[] os = organisms.ToArray();
            Organism[] parents = new Organism[2];
  
            parents[0] = getParent(os);

            do
            {
                parents[1] = getParent(os);
            } while (parents[0] == parents[1]);

            return parents;
        }

        private static Organism getParent(Organism[] os)
        {
            Organism result;
            do {
                result = os[generator.Next(0,os.Length)];
            } while(!result.canReproduce && organisms.Count > 1);
            return result;
        }

        private static void initialPopulation(List<Organism> organisms)
        {
            const int initialPopulationNumber = 20;

            // Specifies the initial blur mutation to apply.
            Mutation blur = new Mutation();
            blur.addGeneticModifier(new Blur(new Set(new[] { "rotation", "position" }), Set.ALL, 0.1));

            for (var i = 0; i < initialPopulationNumber; ++i)
            {
                Organism o = new Test(blur);
                organisms.Add(o);
                o.Died += OrganismDeath;
                Console.WriteLine(o.ToString());
            }

            foreach (Organism o in organisms.ToArray())
                o.start();
        }
	}
}
