/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;

namespace GeneticLibrary.Interfaces
{
    public interface IDeepClonable
    {
        /// <summary>
        /// Do a deep clone of the object.
        /// </summary>
        /// <returns>The clone of the object.</returns>
        Object DeepClone();
    }
}
