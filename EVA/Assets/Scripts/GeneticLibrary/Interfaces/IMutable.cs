/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using GeneticLibrary.Mutations;

namespace GeneticLibrary.Interfaces
{
    interface IMutable
    {
        /// <summary>
        /// Defines the behaviour when the element recieve a mutation.
        /// </summary>
        /// <param name="mutation">The mutation recieved.</param>
        void Accept(IMutation mutation);
    }
}
