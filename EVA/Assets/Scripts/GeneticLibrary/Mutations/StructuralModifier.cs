using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Tools;

namespace GeneticLibrary.Mutations
{
    abstract class StructuralModifier : Modifier
    {
        public StructuralModifier(Set extensions) : base(extensions) { }
        public void Execute(Extension e)
        {
            Action(e);
        }

        abstract protected void Action(Extension e);
    }
}
