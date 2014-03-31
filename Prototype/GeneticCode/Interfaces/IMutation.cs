using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Interfaces
{
    interface IMutation
    {
        void apply(Extension extension);
    }
}
