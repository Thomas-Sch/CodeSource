using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Mutations
{
    abstract class GeneticModifier : Modifier
    {
        protected Set targetedGenes { get; private set; }
        protected Type computeType;

        public GeneticModifier(Set genes, Set extensions, Type computeType)
            : base(extensions)
        {
            this.targetedGenes = genes;
            this.computeType = computeType;
        }

        /// <summary>
        /// Execute the operation on the data.
        /// </summary>
        /// <param name="data"></param>
        public void execute(GeneticData data)
        {
            IList<KeyValuePair<String, IDeepClonable>> updates = new List<KeyValuePair<String, IDeepClonable>>();

            foreach (KeyValuePair<String, IDeepClonable> entry in data)
            {
                // Vérification
                if (targetedGenes.contains(entry.Key) && entry.Value.GetType() == computeType)
                {
                    updates.Add(new KeyValuePair<String, IDeepClonable>(entry.Key, (IDeepClonable)compute(entry.Value)));
                }
            }

            // Applying the updates.
            foreach (KeyValuePair<String, IDeepClonable> entry in updates)
            {
                data.set(entry.Key, entry.Value);
            }
        }

        abstract protected Object compute(Object data);
    }
}
