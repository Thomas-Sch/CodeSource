using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations.Modifiers
{
    class Addition : GeneticModifier
    {
        public Addition(Set genes, Set extensions, double value) : base(genes, extensions, value) { }

        override protected Vector3 compute(Vector3 data)
        {
            data.x += value;
            data.y += value;
            data.z += value;

            return data;
        }
    }
}
