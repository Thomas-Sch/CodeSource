using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Wrappers;

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
            base.set("position", new WVector3());
            base.set("rotation", new WVector3());
            base.set("scale", new WVector3(1, 1, 1));
        }
    }
}
