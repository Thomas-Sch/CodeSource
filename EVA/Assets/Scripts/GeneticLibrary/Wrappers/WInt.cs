/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using GeneticLibrary.Interfaces;

namespace GeneticLibrary.Wrappers
{
    class WInt : IDeepClonable
    {
        public int Value { get; set; }

        public WInt(int value)
        {
            this.Value = value;
        }

        public object DeepClone()
        {
            return new WInt(Value);
        }

        public override String ToString()
        {
            return Value.ToString();
        }
    }
}
