using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GeneticLibrary.Interfaces;
using UnityEngine;

namespace GeneticLibrary
{
    /// <summary>
    /// Entry class to interact with the organism.
    /// </summary>
    public abstract class Organism
    {
        /// <summary>
        /// Used to differenciate organisms.
        /// </summary>
        private static int counter;
        public int OrganismNumber { get; private set; }

        public Boolean CanReproduce {get; set;}

        public Genotype Genotype { get; protected set; }

        static Organism()
        {
            counter = 0;
        }

        /// <summary>
        /// Create an empty organism with empty genotype.
        /// </summary>
        protected Organism()
        {
            OrganismNumber = ++counter;
        }

        /// <summary>
        /// Causes the organism to mutate accordingly to the settings in the mutation.
        /// </summary>
        /// <param name="m">The mutation</param>
        public void Mutate(IMutation m)
        {
            Genotype.Mutate(m);
        }

        /// <summary>
        /// Create an empty organism with no geneticData and no extensions.
        /// </summary>
        /// <returns></returns>
        public abstract Organism CreateEmpty();

        override public string ToString()
        {
            return "I'm organism - " + OrganismNumber + ":" + Environment.NewLine + Genotype.ToString();
        }
    }
}
