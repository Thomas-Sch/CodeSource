/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>
///
using System;
using System.Collections;
using System.Collections.Generic;


namespace GeneticLibrary.Recombination
{
    /// <summary>
    /// Defines the functions that a recombinaison class must have.
    /// </summary>
	public class RecombinationOutput
	{
		private IList<Genotype> Genotypes;

		public int Count { get {
				return Genotypes.Count;
			}}

		public RecombinationOutput() {
			Genotypes = new List<Genotype>();
		}

		public RecombinationOutput(IList<Genotype> genotypes) {
			Genotypes = genotypes;
		}

		public void Add(Genotype genotype) {
			Genotypes.Add(genotype);
		}

		public IEnumerator GetEnumerator()
		{
			return Genotypes.GetEnumerator();
		}
	}
}

