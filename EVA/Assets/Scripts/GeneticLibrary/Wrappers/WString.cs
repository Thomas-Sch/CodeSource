using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Wrappers
{
    class WString : IDeepClonable
    {
        public String Value { get; set; }

        public WString(String value)
        {
            this.Value = value;
        }

        public object DeepClone()
        {
            return new WString((String)Value.Clone());
        }

        public override String ToString() { return Value; }
    }
}
