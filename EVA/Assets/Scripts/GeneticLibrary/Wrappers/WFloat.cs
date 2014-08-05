/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using GeneticLibrary.Interfaces;

namespace GeneticLibrary
{
	public class WFloat : IDeepClonable
	{
		public float Value {get; set;}

		public WFloat (float f)
		{
			Value = f;
		}

		public object DeepClone ()
		{
			return new WFloat(Value);
		}

		public override string ToString() {
			return Value.ToString();
		}
	}
}