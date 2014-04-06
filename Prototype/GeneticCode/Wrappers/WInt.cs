using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Wrappers
{
    class WInt : IDeepClonable
    {
        public int value { get; set; }

        public WInt(int value)
        {
            this.value = value;
        }

        public object deepClone()
        {
            return new WInt(value);
        }

        public override String ToString()
        {
            return value.ToString();
        }
    }
}
