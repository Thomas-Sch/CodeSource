using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Wrappers;
using UnityEngine;

namespace GeneticCode.Mutations.Modifiers
{
    class Blur : GeneticModifier
    {
        private static System.Random generator;
        float value;

        static Blur()
        {
            generator = new System.Random();
        }


		public Blur(Set extensions, Set genes, float value) : base(extensions, genes, typeof(WVector3)) { this.value = value; }

        protected override object Compute(object o)
        {
            WVector3 data = (WVector3)o;
			data.Value = new Vector3(mix (data.x, value), mix (data.y, value), mix (data.z, value));
            return data;
        }

        private float mix(float original, float delta)
        {
            int sign = (float)generator.NextDouble() < 0.5 ? 1 : -1;
			original += sign * (float) generator.NextDouble() * delta;
            return original;
        }
    }
}
