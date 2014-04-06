using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode
{
    abstract class BodyPart : Extension
    {
        protected BodyPart() : base() { }
        public BodyPart(String tag) : base(tag) { }
        public BodyPart(GeneticData data) : base(data) { }
    }
}
