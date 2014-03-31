using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Extensions.BodyParts
{
    class Sphere : BodyPart
    {
        public Sphere() : base() { }
        public Sphere(String tag) : base(tag) { }
        public Sphere(GeneticData data) : base(data: data) { }

        protected override Extension copyNode()
        {
            return new Sphere(data: null);
        }
    }
}
