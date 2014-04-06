using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Extensions.BodyParts
{
    class Sphere : BodyPart
    {
        protected Sphere() : base() { }
        public Sphere(String tag) : base(tag) { }
        public Sphere(GeneticData data) : base(data) { }

        protected override Extension localCloneImpl()
        {
            return new Sphere();
        }
    }
}
