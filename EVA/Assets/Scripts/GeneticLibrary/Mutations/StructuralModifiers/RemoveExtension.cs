 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations.StructuralOperations
{
    class RemoveExtension : StructuralModifier
    {
        public RemoveExtension(Set extensions) : base(extensions) { }

        override protected void Action(Extension e) {
            if (e.Parent != null)
            {
                Console.WriteLine("My parent is " + e.Parent.Tag + "!");
                e.Parent.RemoveExtension(e);
            }
        }
    }
}
