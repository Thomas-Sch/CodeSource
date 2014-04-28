/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;

namespace GeneticLibrary.Extensions.Members
{
    class Tubular : Member
    {
        protected Tubular() : base() { }
        public Tubular(String tag) : base(tag) { }
        public Tubular(GeneticData data) : base(data) { }

        protected override Extension LocalCloneImpl()
        {
            return new Tubular();
        }
    }
}
