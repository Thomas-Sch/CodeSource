/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using GeneticLibrary.BodyParts;
using GeneticLibrary.Extensions.BodyParts;
using GeneticLibrary.Extensions.Members;
using GeneticLibrary.Interfaces;

namespace GeneticLibrary
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
