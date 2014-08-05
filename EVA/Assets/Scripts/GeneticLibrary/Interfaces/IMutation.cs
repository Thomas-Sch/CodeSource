/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

namespace GeneticLibrary.Interfaces
{
    public interface IMutation
    {
        /// <summary>
        /// Apply the mutation on the extension.
        /// </summary>
        /// <param name="extension">The extension</param>
        void Apply(Extension extension);
    }
}
