/// <summary>
/// This file is part of the GeneticLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using GeneticLibrary.Interfaces;
using GeneticLibrary.Mutations;

namespace GeneticLibrary
{
    /// <summary>
    /// Represent the genetic data of a node in the genetic structure of the organism. The genes are handled with a key/value system.
    /// </summary>
    public class GeneticData : IEnumerable, IDeepClonable
    {
        // Tag of the geneticData.
        public String tag { get; set; }

        private Dictionary<String, IDeepClonable> data = new Dictionary<String, IDeepClonable>();

        public GeneticData() : this("") {}

        public GeneticData(String s)
        {
            tag = s;
        }

        /// <summary>
        /// Add or modifiy a gene.
        /// </summary>
        /// <param name="element">The name of the gene</param>
        /// <param name="data">The value of the gene</param>
        public void Set(String element, IDeepClonable data)
        {
            this.data[element] = data;
        }

        public IDeepClonable Get(String element) {
            return this.data[element];
        }

        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        override public string ToString()
        {
            string result = "";
            foreach (KeyValuePair<String, IDeepClonable> entry in data)
            {
                result += entry.ToString() + " ";
            }
            return result;
        }

        public Object DeepClone()
        {
            var clone = new GeneticData(tag);
            foreach (KeyValuePair<String, IDeepClonable> entry in data)
            {
                clone.Set((String) entry.Key.Clone(), (IDeepClonable) entry.Value.DeepClone());
            }
            return clone;
        }
    }
}
