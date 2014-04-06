using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Wrappers
{
    class WString : IDeepClonable
    {
        public String value { get; set; }

        public WString(String value)
        {
            this.value = value;
        }

        public object deepClone()
        {
            return new WString((String)value.Clone());
        }

        public override String ToString() { return value; }
    }
}
