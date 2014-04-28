/// <summary>
/// This file is part of the GenotypeLibrary wich aims to
/// represent a genotype and gives the tools to modify it.
/// 
/// Author : Thomas Schweizer
/// Date   : March 2014
/// </summary>

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GeneticLibrary.Interfaces;

namespace GeneticLibrary
{
    /// <summary>
    /// Represents the structure in the genotype. Each part of the organism is called "Extension" and contains data
    /// about his structure in a tree shape and the data relative to each extension.
    /// </summary>
    public abstract class Extension : IMutable, IDeepClonable, IEnumerable
    {
        /// <summary>
        /// Parent of the extension (it's a tree shaped structure).
        /// </summary>
        public Extension Parent { get; private set; }

        /// <summary>
        /// Sons of the extension (that's here that the structure is stored).
        /// </summary>
        private IList<Extension> extensions = new List<Extension>();

        /// <summary>
        /// Datas relative to this extension. Contains genes. These
        /// are information with a name (to retrieve and use it) and a data.
        /// </summary>
        public GeneticData GeneticData { get; private set; }

        /// <summary>
        /// Name of the extension.
        /// </summary>
        public String Tag
        {
            get { return GeneticData.tag; }
			set { GeneticData.tag = value;}
        }

        /// <summary>
        /// Used for copy. Does nothing.
        /// </summary>
        protected Extension() { }

        /// <summary>
        /// Instanciate a named extension.
        /// </summary>
        /// <param name="tag">name of the extension or 'tag'</param>
        public Extension(string tag)
        {
            GeneticData = new GeneticData(tag);
        }

        /// <summary>
        /// Affects data to the genetic data of the extension.
        /// </summary>
        /// <param name="data"></param>
        public Extension(GeneticData data)
        {
            GeneticData = data;
        }

        /// <summary>
        /// Return the number of child extensions for this node.
        /// </summary>
        /// <returns>The number of child extensions</returns>
        public int GetNumberOfChildExtensions()
        {
            return extensions.Count();
        }

        /// <summary>
        /// Add an extension as a son.
        /// </summary>
        /// <param name="extension">New son.</param>
        public void AddExtension(Extension extension)
        {
            extensions.Add(extension);
            extension.SetParent(this);
        }

        /// <summary>
        /// Remove a child of this extension.
        /// </summary>
        /// <param name="e">The extension to remove</param>
        public void RemoveExtension(Extension e)
        {
            extensions.Remove(e);
        }

        /// <summary>
        /// Set the parent of the extension.
        /// </summary>
        /// <param name="parent">The parent</param>
        private void SetParent(Extension parent)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Add or modify a specific gene in the extension.
        /// </summary>
        /// <param name="tag">Name of the gene</param>
        /// <param name="data">Data associated</param>
        public void SetGeneticData(string tag, IDeepClonable data)
        {
            GeneticData.Set(tag, data);
        }

        public String ToString(int level)
        {
            string result = this.GetType().Name + "(" + Tag + ")" + " " + GeneticData.ToString();
            foreach (Extension extension in extensions) {
                result += Environment.NewLine;
                for(int i = 0; i < level; i++)
                    result += "   ";
                result += extension.ToString(level + 1);
            }
            return result;
        }

        /// <summary>
        /// Apply the mutation on the extension and his childs.
        /// </summary>
        /// <param name="mutation">The mutation</param>
        public void Accept(IMutation mutation)
        {
            // Shortcuts can be made here to gain in performance.

            // Apply mutation locally first.
            mutation.Apply(this);

            IList<Extension> copy = extensions.ToList<Extension>();

            // Propagate in lower levels.
            foreach (Extension extension in copy)
            {
                extension.Accept(mutation);
            } 
        }

        /// <summary>
        /// Clone the extension without the children.
        /// </summary>
        /// <returns>The copy</returns>
        public Extension LocalClone()
        {
            Extension result = LocalCloneImpl();
            result.GeneticData = (GeneticData) GeneticData.DeepClone();
            return result;
        }

        /// <summary>
        /// Return an empty node of the current type.
        /// </summary>
        /// <returns>An empty subclass of Extention</returns>
        protected abstract Extension LocalCloneImpl();


        /// <summary>
        /// Clone the extension with the children.
        /// </summary>
        /// <returns>The copy</returns>
        public Object DeepClone()
        {
            Extension result = LocalClone();
            foreach (Extension e in extensions)
            {
                result.AddExtension((Extension)e.DeepClone());
            }
            return result;
        }

		/// <summary>
		/// Returns an enumerator that iterates through the childs of this extension.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator GetEnumerator ()
		{
			return extensions.GetEnumerator();
		}
    }
}