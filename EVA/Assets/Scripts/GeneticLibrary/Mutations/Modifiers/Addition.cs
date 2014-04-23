using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Wrappers;
using UnityEngine;
using GeneticLibrary.Tools;

namespace GeneticLibrary.Mutations.Modifiers
{
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
