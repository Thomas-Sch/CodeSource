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
    class Square : BodyPart
    {
        protected Square() : base() { }
        public Square(String tag) : base(tag) { }
        public Square(GeneticData data) : base(data) { }

        protected override Extension LocalCloneImpl()
        {
            return new Square();
        }
    }
}
