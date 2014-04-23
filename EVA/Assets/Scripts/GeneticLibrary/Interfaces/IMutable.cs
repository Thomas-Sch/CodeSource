using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Mutations;

namespace GeneticLibrary.Interfaces
{
    interface IMutable
    {
        void Accept(IMutation mutation);
    }
}
