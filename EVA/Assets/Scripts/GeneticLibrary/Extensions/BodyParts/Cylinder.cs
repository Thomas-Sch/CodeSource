using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.BodyParts
{
    class Cylinder : BodyPart
    {
        protected Cylinder() : base() { }
        public Cylinder(String tag) : base(tag) { }
        public Cylinder(GeneticData data) : base(data) { }
        protected override Extension LocalCloneImpl()
        {
            return new Cylinder(); 
        }
    }
}
