/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;

namespace GeneticLibrary
{
    abstract class BodyPart : Extension
    {
        protected BodyPart() : base() { }
        public BodyPart(String tag) : base(tag) { }
        public BodyPart(GeneticData data) : base(data) { }
    }
}
