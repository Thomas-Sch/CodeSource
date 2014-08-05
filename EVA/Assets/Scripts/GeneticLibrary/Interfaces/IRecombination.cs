/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

namespace GeneticLibrary.Recombination
{
   	/// <summary>
    /// This class implement the singleton model. Each subclass have his own singleton.
    /// </summary>
    interface IRecombination
    {
        /// <summary>
        /// Create offsprings from parent a and b.
        /// </summary>
        /// <param name="a">Parent a</param>
        /// <param name="b">Parent b</param>
        /// <returns>The offsprings</returns>
        RecombinationOutput Recombine(Genotype a, Genotype b);
    }
}
