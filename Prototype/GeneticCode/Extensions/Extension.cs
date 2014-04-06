using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;

namespace GeneticCode
{
    /// <summary>
    /// Represents the structure in the genotype. Each part of the organism is called "Extension" and contains data
    /// about his structure in a tree shape and the data relative to each extension.
    /// </summary>
    abstract class Extension : IMutable, IDeepClonable
    {
        /// <summary>
        /// Parent of the extension (it's a tree shaped structure).
        /// </summary>
        public Extension parent { get; private set; }

        /// <summary>
        /// Sons of the extension (that's here that the structure is stored).
        /// </summary>
        private IList<Extension> extensions = new List<Extension>();

        /// <summary>
        /// Datas relative to this extension. Contains genes. These
        /// are information with a name (to retrieve and use it) and a data.
        /// </summary>
        public GeneticData geneticData { get; private set; }

        /// <summary>
        /// Name of the extension.
        /// </summary>
        public String tag
        {
            get { return geneticData.tag; }
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
            geneticData = new GeneticData(tag);
        }

        /// <summary>
        /// Affects data to the genetic data of the extension.
        /// </summary>
        /// <param name="data"></param>
        public Extension(GeneticData data)
        {
            geneticData = data;
        }

        /// <summary>
        /// Return the number of child extensions for this node.
        /// </summary>
        /// <returns>The number of child extensions</returns>
        public int getNumberOfChildExtensions()
        {
            return extensions.Count();
        }

        /// <summary>
        /// Add an extension as a son.
        /// </summary>
        /// <param name="extension">New son.</param>
        public void addExtension(Extension extension)
        {
            extensions.Add(extension);
            extension.setParent(this);
        }

        /// <summary>
        /// Remove a child of this extension.
        /// </summary>
        /// <param name="e">The extension to remove</param>
        public void removeExtension(Extension e)
        {
            extensions.Remove(e);
        }

        /// <summary>
        /// Set the parent of the extension.
        /// </summary>
        /// <param name="parent">The parent</param>
        private void setParent(Extension parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Add or modify a specific gene in the extension.
        /// </summary>
        /// <param name="tag">Name of the gene</param>
        /// <param name="data">Data associated</param>
        public void setGeneticData(string tag, IDeepClonable data)
        {
            geneticData.set(tag, data);
        }

        public String ToString(int level)
        {
            string result = this.GetType().Name + "(" + tag + ")" + " " + geneticData.ToString();
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
        public void accept(IMutation mutation)
        {
            // Shortcuts can be made here to gain in performance.

            // Apply mutation locally first.
            mutation.apply(this);

            IList<Extension> copy = extensions.ToList<Extension>();

            // Propagate in lower levels.
            foreach (Extension extension in copy)
            {
                extension.accept(mutation);
            } 
        }

        /// <summary>
        /// Clone the extension without the children.
        /// </summary>
        /// <returns>The copy</returns>
        public Extension localClone()
        {
            Extension result = localCloneImpl();
            result.geneticData = (GeneticData) geneticData.deepClone();
            return result;
        }

        /// <summary>
        /// Return an empty node of the current type.
        /// </summary>
        /// <returns>An empty subclass of Extention</returns>
        protected abstract Extension localCloneImpl();


        /// <summary>
        /// Clone the extension with the children.
        /// </summary>
        /// <returns>The copy</returns>
        public Object deepClone()
        {
            Extension result = localClone();
            foreach (Extension e in extensions)
            {
                result.addExtension((Extension)e.deepClone());
            }
            return result;
        }

        /// <summary>
        /// Returns the enumerator of the child list.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator getChildsEnumerator()
        {
            return extensions.GetEnumerator();
        }
    }
}