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
    abstract class Member : Extension
    {
        protected Member() : base() { }
        public Member(String tag) : base(tag) { }
        public Member(GeneticData data) : base(data) { }
    }
}
