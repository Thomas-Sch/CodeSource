/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using System.Collections.Generic;
using GeneticLibrary.Interfaces;

namespace GeneticLibrary.Wrappers
{
    class WString : IDeepClonable
    {
        public String Value { get; set; }

        public WString(String value)
        {
            this.Value = value;
        }

        public object DeepClone()
        {
            return new WString((String)Value.Clone());
        }

        public override String ToString() { return Value; }
    }
}
