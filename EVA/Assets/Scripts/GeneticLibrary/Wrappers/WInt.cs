using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Interfaces;

namespace GeneticLibrary.Wrappers
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
