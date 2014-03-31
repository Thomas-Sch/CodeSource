using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.GeneticDataTemplates
{
    /// <summary>
    /// Set default genes for a new Organism.
    /// </summary>
    class DefaultSEVA : GeneticData
    {
        public DefaultSEVA() : base() { initialize(); }
        public DefaultSEVA(string tag) : base(tag) { initialize(); }

        private void initialize() {
            // Base template for Extensions.
            base.set("position", new Vector3());
            base.set("rotation", new Vector3());
            base.set("scale", new Vector3(1, 1, 1));
        }
    }
}
