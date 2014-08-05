/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;
using GeneticLibrary.Collections;
using Wrappers;
using GeneticLibrary.Mutations;

namespace GeneticCode.Mutations.GeneticModifiers
{
    /// <summary>
    /// Define a mutation which adds value to others.
    /// </summary>
    class Addition : GeneticModifier
    {
        private float value;
		public Addition(Set extensions, Set genes, float value) : base(extensions, genes, typeof(WVector3)) { this.value = value; }

        override protected object Compute(object o)
        {
            WVector3 data = (WVector3)o;
			data.Value = new Vector3(data.Value.x + value, data.Value.y + value, data.Value.z + value);
            return data;
        }
    }
}
