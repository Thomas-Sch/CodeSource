using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations
{
    class Modifier
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
