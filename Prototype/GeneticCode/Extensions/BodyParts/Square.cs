using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.BodyParts
{
    class Square : BodyPart
    {
        public Square() : base() { }
        public Square(String tag) : base(tag) { }
        public Square(GeneticData data) : base(data) { }

        protected override Extension copyNode()
        {
            return new Square(data:null);
        }
    }
}
