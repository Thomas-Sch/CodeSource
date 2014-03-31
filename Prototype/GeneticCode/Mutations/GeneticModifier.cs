using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations
{
    abstract class GeneticModifier : Modifier
    {
        protected double value { get; private set; }
        protected Set targetedGenes { get; private set; }

        public GeneticModifier(Set genes, Set extensions, double value)
            : base(extensions)
        {
            this.targetedGenes = genes;
            this.value = value;
        }

        /// <summary>
        /// Execute the operation on the data.
        /// </summary>
        /// <param name="data"></param>
        public void execute(GeneticData data)
        {
            IList<KeyValuePair<String, Vector3>> updates = new List<KeyValuePair<String, Vector3>>();

            foreach (KeyValuePair<String, Vector3> entry in data)
            {
                if (targetedGenes.contains(entry.Key))
                {
                    updates.Add(new KeyValuePair<String, Vector3>(entry.Key, compute(entry.Value)));
                }
            }

            // Applying the updates.
            foreach (KeyValuePair<string, Vector3> entry in updates)
            {
                data.set(entry.Key, entry.Value);
            }
        }

        abstract protected Vector3 compute(Vector3 data);
    }
}
