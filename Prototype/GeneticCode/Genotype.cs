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
        private Extension rootElement { get; set; }

        /// <summary>
        /// Message when genotype is empty or not set.
        /// </summary>
        private const String errorEmptyGenotype = "The genotype is empty";

        /// <summary>
        /// Causes the organism to mutate accordingly to the settings in the mutation.
        /// </summary>
        /// <param name="m">The mutation</param>
        public void mutate(IMutation m)
        {
            if (rootElement == null)
                throw new NullReferenceException(errorEmptyGenotype);
            rootElement.accept(m);
        }

        /// <summary>
        /// Set the root element of the structure of the genotype
        /// </summary>
        /// <param name="e"></param>
        public void setRootElement(Extension e)
        {
            rootElement = e;
        }

        /// <summary>
        /// Returns the root element.
        /// </summary>
        /// <returns>The root element</returns>
        public Extension getRootElement()
        {
            if (rootElement == null)
                throw new NullReferenceException(errorEmptyGenotype);
            return rootElement;
        }

        override public String ToString()
        {
            return (rootElement != null ? rootElement.ToString(1) : errorEmptyGenotype);
        }
    }
}
