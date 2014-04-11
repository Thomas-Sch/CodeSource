using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Mutations;

namespace GeneticCode.Interfaces
{
    interface IMutable
    {
        void Accept(IMutation mutation);
    }
}
