using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode.Mutations.Modifiers
{
    class Blur : GeneticModifier
    {
        private static Random generator;

        static Blur()
        {
            generator = new Random();
        }


        public Blur(Set genes, Set extensions, double value) : base(genes, extensions, value) { }

        protected override Vector3 compute(Vector3 data)
        {
            data.x = mix(data.x, value);
            data.y = mix(data.y, value);
            data.z = mix(data.z, value);
            return data;
        }

        private double mix(double original, double delta)
        {
            int sign = generator.NextDouble() < 0.5 ? 1 : -1;
            original += sign * generator.NextDouble() * delta;
            return original;
        }
    }
}
