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
    public class Genotype
    {
		private Extension rootElement;
        /// <summary>
        /// First element in the structure of the genotype.
        /// </summary>
		public Extension RootElement { 
			get {           
				if (rootElement == null)
					throw new NullReferenceException(errorEmptyGenotype);
				return rootElement;
			} 

			set {
				rootElement = value;
			}
		}

        /// <summary>
        /// Message when genotype is empty or not set.
        /// </summary>
        private const String errorEmptyGenotype = "The genotype is empty";

        /// <summary>
        /// Causes the organism to mutate accordingly to the settings in the mutation.
        /// </summary>
        /// <param name="m">The mutation</param>
        public void Mutate(IMutation m)
        {
            RootElement.Accept(m);
        }
	

        override public String ToString()
        {
            return (rootElement != null ? RootElement.ToString(1) : errorEmptyGenotype);
        }
    }
}
