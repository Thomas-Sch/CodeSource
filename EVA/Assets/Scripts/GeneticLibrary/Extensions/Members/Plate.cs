using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Extensions.Members
{
    class Plate : Member
    {
        protected Plate() : base() { }
        public Plate(String tag) : base(tag) { }
        public Plate(GeneticData data) : base(data) { }

        protected override Extension LocalCloneImpl()
        {
            return new Plate();
        }
    }
}
