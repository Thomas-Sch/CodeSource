using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Wrappers;

namespace GeneticCode.Mutations.Modifiers
{
    class Multiplication : GeneticModifier
    {
        private double value;
        public Multiplication(Set genes, Set extensions, double value) : base(genes, extensions, typeof(WVector3)) { this.value = value; }

        override protected Object compute(Object o)
        {
            WVector3 data = (WVector3)o;
            data.x *= value;
            data.y *= value;
            data.z *= value;

            return data;
        }
    }
}
