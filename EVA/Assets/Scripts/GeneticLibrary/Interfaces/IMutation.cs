using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLibrary.Interfaces
{
    public interface IMutation
    {
        void Apply(Extension extension);
    }
}
