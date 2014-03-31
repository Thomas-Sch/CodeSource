 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations.StructuralOperations
{
    class RemoveExtension : StructuralModifier
    {
        public RemoveExtension(Set extensions) : base(extensions) { }

        override protected void action(Extension e) {
            if (e.parent != null)
            {
                Console.WriteLine("My parent is " + e.parent.tag + "!");
                e.parent.removeExtension(e);
            }
        }
    }
}
