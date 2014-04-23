using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLibrary.Recombination
{
    /// <summary>
    /// This class implement the singleton model. Each subclass have his own singleton.
    /// </summary>
    abstract class RecombinationHandler
    {
        /// <summary>
        /// The instance of each class.
        /// </summary>
        protected static RecombinationHandler instance;

        /// <summary>
        /// Create offsprings from parent a and b.
        /// </summary>
        /// <param name="a">Parent a</param>
        /// <param name="b">Parent b</param>
        /// <returns>The offsprings</returns>
        abstract public Genotype[] Recombine(Genotype a, Genotype b);
    }
}
