using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GeneticLibrary.Interfaces;
using GeneticLibrary.Tools;

namespace GeneticLibrary.Mutations
{
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
