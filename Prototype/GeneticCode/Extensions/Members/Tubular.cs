using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Extensions.Members
{
    class Tubular : Member
    {
        protected Tubular() : base() { }
        public Tubular(String tag) : base(tag) { }
        public Tubular(GeneticData data) : base(data) { }

        protected override Extension localCloneImpl()
        {
            return new Tubular();
        }
    }
}
