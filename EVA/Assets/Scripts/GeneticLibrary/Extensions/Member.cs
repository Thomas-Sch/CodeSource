using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLibrary
{
    abstract class Member : Extension
    {
        protected Member() : base() { }
        public Member(String tag) : base(tag) { }
        public Member(GeneticData data) : base(data) { }
    }
}
