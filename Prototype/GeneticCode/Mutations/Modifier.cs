using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations
{
    class Modifier
    {
        protected Set targetedExtensions { get; private set; }

        public Modifier() : this(Set.ALL) { }

        public Modifier(Set targetedExtensions)
        {
            this.targetedExtensions = targetedExtensions;
        }

        public bool targetsExtension(Extension e)
        {
            return targetedExtensions.contains(e.tag) || targetedExtensions.contains(e.GetType().Name) || targetedExtensions.contains(e.GetType().Name + " " + e.tag);
        }
    }
}
