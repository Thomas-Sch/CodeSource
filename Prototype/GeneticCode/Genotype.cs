using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.BodyParts;
using GeneticCode.Extensions.BodyParts;
using GeneticCode.Extensions.Members;
using GeneticCode.Interfaces;

namespace GeneticCode
{
    class Genotype
    {
        /// <summary>
        /// First element in the structure of the genotype.
        /// </summary>
        public BodyPart rootElement { get; set; }

        /// <summary>
        /// Causes the organism to mutate accordingly to the settings in the mutation.
        /// </summary>
        /// <param name="m">The mutation</param>
        public void mutate(IMutation m)
        {
            rootElement.accept(m);
        }

        override public String ToString()
        {
            return rootElement.ToString(1);
        }
    }
}
