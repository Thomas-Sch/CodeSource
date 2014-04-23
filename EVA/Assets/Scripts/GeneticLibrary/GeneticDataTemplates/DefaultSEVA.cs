using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Wrappers;

namespace GeneticLibrary.GeneticDataTemplates
{
    /// <summary>
    /// Set default genes for a new Organism.
    /// </summary>
    class DefaultSEVA : GeneticData
    {
        public DefaultSEVA() : base() { Initialize(); }
        public DefaultSEVA(string tag) : base(tag) { Initialize(); }

        private void Initialize() {
            // Base template for Extensions.
            base.Set("position", new WVector3());
            base.Set("rotation", new WVector3());
            base.Set("scale", new WVector3(1, 1, 1));
        }
    }
}
