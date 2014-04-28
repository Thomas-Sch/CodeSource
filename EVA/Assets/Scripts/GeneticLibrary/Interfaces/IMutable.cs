/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
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
        void Accept(IMutation mutation);
    }
}
