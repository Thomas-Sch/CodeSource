/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>
/// 
using System;
using System.Collections.Generic;
using GeneticLibrary.Collections;
using GeneticLibrary.Mutations;
using UnityEngine;
using Wrappers;

namespace GeneticCode.Mutations.GeneticModifiers
{
    /// <summary>
    /// Mutation which multiply values.
    /// </summary>
    class Multiplication : GeneticModifier
    {
        private float value;
		public Multiplication(Set extensions, Set genes, float value) : base(extensions, genes, typeof(WVector3)) { this.value = value; }

        override protected object Compute(object o)
        {
            WVector3 data = (WVector3)o;
			data.Value = new Vector3(data.x * value, data.y * value, data.z * value);
            return data;
        }
    }
}
