/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using GeneticLibrary.Collections;

namespace GeneticLibrary.Mutations
{
    /// <summary>
    /// Represents a modifier of a mutation.
    /// </summary>
    public abstract class Modifier
    {
        protected Set TargetedExtensions { get; private set; }

        public Modifier() : this(Set.ALL) { }

        public Modifier(Set targetedExtensions)
        {
            this.TargetedExtensions = targetedExtensions;
        }

        public bool TargetsExtension(Extension e)
        {
            return TargetedExtensions.Contains(e.Tag) || TargetedExtensions.Contains(e.GetType().Name) || TargetedExtensions.Contains(e.GetType().Name + " " + e.Tag);
        }
    }
}
