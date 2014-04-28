/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;

namespace GeneticLibrary.BodyParts
{
    class Cylinder : BodyPart
    {
        protected Cylinder() : base() { }
        public Cylinder(String tag) : base(tag) { }
        public Cylinder(GeneticData data) : base(data) { }
        protected override Extension LocalCloneImpl()
        {
            return new Cylinder(); 
        }
    }
}
