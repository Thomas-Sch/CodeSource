/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;

namespace GeneticLibrary.Extensions.BodyParts
{
    class Sphere : BodyPart
    {
        protected Sphere() : base() { }
        public Sphere(String tag) : base(tag) { }
        public Sphere(GeneticData data) : base(data) { }

        protected override Extension LocalCloneImpl()
        {
            return new Sphere();
        }
    }
}
