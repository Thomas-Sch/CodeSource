/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using System.Collections.Generic;
using GeneticLibrary.Interfaces;
using GeneticLibrary.Collections;

namespace GeneticLibrary.Mutations
{
    /// <summary>
    /// Represent a genetic modifier of a mutation.
    /// </summary>
    public abstract class GeneticModifier : Modifier
    {
        protected Set TargetedGenes { get; private set; }
        protected Type computeType;

		public GeneticModifier(Set extensions, Set genes, Type computeType)
            : base(extensions)
        {
            this.TargetedGenes = genes;
            this.computeType = computeType;
        }

        /// <summary>
        /// Execute the operation on the data.
        /// </summary>
        /// <param name="data"></param>
        public void Execute(GeneticData data)
        {
            IList<KeyValuePair<String, IDeepClonable>> updates = new List<KeyValuePair<String, IDeepClonable>>();

            foreach (KeyValuePair<String, IDeepClonable> entry in data)
            {
                // Vérification
                if (TargetedGenes.Contains(entry.Key) && computeType == entry.Value.GetType())
                {
                    updates.Add(new KeyValuePair<String, IDeepClonable>(entry.Key, (IDeepClonable)Compute(entry.Value)));
                }
            }

            // Applying the updates.
            foreach (KeyValuePair<String, IDeepClonable> entry in updates)
            {
                data.Set(entry.Key, entry.Value);
            }
        }

        abstract protected Object Compute(Object data);
    }
}
