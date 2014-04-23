using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Wrappers;
using GeneticLibrary.Tools;
using UnityEngine;

namespace GeneticLibrary.Mutations.Modifiers
{
    public class Blur : GeneticModifier
    {
        private static System.Random generator;
        public float Value {get; set;}
		public System.Type type {get; set;}

        static Blur()
        {
            generator = new System.Random();
        }

		public Blur(Set extensions, Set genes, float value) : base(extensions, genes, typeof(WVector3)) { this.Value = value; }
		public Blur(Set extensions, Set genes, System.Type type, float value) : base(extensions, genes, type) {
			this.Value = value;
		}

        protected override object Compute(object o)
        {
            WVector3 data = (WVector3)o;
			data.Value = new Vector3(mix (data.x, Value), mix (data.y, Value), mix (data.z, Value));
            return data;
        }

        protected float mix(float original, float delta)
        {
            int sign = (float)generator.NextDouble() < 0.5 ? 1 : -1;
			original += sign * (float) generator.NextDouble() * delta;
            return original;
        }
    }
}
