using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations
{
    abstract class StructuralModifier : Modifier
    {
        public StructuralModifier(Set extensions) : base(extensions) { }
        public void execute(Extension e)
        {
            action(e);
        }

        abstract protected void action(Extension e);
    }
}
