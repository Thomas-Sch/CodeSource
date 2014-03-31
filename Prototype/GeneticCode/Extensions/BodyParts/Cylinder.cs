using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.BodyParts
{
    class Cylinder : BodyPart
    {
        public Cylinder() : base() { }
        public Cylinder(String tag) : base(tag) { }
        public Cylinder(GeneticData data) : base(data: data) { }
        protected override Extension copyNode()
        {
            return new Cylinder(data: null); 
        }
    }
}
