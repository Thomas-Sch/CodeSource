using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Interfaces
{
    public interface IMutation
    {
        void Apply(Extension extension);
    }
}
