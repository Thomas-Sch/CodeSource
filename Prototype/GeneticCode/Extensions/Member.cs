using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode
{
    abstract class Member : Extension
    {
        public Member() : base() { }
        public Member(String tag) : base(tag) { }
        public Member(GeneticData data) : base(data) { }
    }
}
