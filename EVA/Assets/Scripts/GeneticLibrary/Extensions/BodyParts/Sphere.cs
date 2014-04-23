using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLibrary.Extensions.BodyParts
{
    class Sphere : BodyPart
    {
        protected Sphere() : base() { }
        public Sphere(String tag) : base(tag) { }
        public Sphere(GeneticData data) : base(data) { }

        protected override Extension LocalCloneImpl()
        {
            return new Sphere();
        }
    }
}
