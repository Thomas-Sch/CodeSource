/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>
/// 
using System;
using GeneticLibrary;
using GeneticLibrary.Collections;
using GeneticLibrary.Wrappers;

namespace GeneticCode.Mutations.GeneticModifiers
{
    /// <summary>
    /// Mutation which blur the value on a single value and not a Vector3 like the others mutations.
    /// </summary>
	public class SingleBlur : Blur
	{
		public SingleBlur(Set extensions, Set genes, float value) : base(extensions, genes, typeof(WFloat), value) { 
		}

		protected override object Compute(object o)
		{
			WFloat data = (WFloat)o;
			data.Value = new WFloat(mix(data.Value, Value)).Value;
			return data;
		}
	}
}