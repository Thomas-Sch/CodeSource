using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode.Wrappers
{
    class WVector3 : Vector3, IDeepClonable
    {
        public WVector3(double x, double y, double z) : base(x, y, z) { }
        public WVector3() : base() { }
        public object deepClone()
        {
            return new WVector3(x, y, z);
        }
    }
}
