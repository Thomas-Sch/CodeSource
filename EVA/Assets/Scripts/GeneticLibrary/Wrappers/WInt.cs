using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Wrappers
{
    class WInt : IDeepClonable
    {
        public int Value { get; set; }

        public WInt(int value)
        {
            this.Value = value;
        }

        public object DeepClone()
        {
            return new WInt(Value);
        }

        public override String ToString()
        {
            return Value.ToString();
        }
    }
}
