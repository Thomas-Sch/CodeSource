/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using GeneticLibrary.Collections;

namespace GeneticLibrary.Mutations
{
    abstract class StructuralModifier : Modifier
    {
        public StructuralModifier(Set extensions) : base(extensions) { }
        public void Execute(Extension e)
        {
            Action(e);
        }

        abstract protected void Action(Extension e);
    }
}
