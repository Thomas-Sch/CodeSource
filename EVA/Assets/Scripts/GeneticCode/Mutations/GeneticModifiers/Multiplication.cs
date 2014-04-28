using System;
using System.Collections.Generic;
using GeneticLibrary.Collections;
using UnityEngine;
using Wrappers;

namespace GeneticLibrary.Mutations.GeneticModifiers
{
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
