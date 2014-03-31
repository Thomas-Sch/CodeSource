using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticCode
{
    /// <summary>
    /// Represent a set of element.
    /// This class gives several handy basic sets.
    /// </summary>
    class Set
    {
        /// <summary>
        /// Whitelist : nothing is taken by default.
        /// Blacklist : everything is taken by default.
        /// </summary>
        public enum Mode { Whitelist, Blacklist };

        /// <summary>
        /// This set includes everything.
        /// </summary>
        readonly public static Set ALL = new Set(Mode.Blacklist);

        /// <summary>
        /// This set excludes all elements.
        /// </summary>
        readonly public static Set NONE = new Set(Mode.Whitelist);

        HashSet<string> elements;

        public Mode mode { private get; set; }

        /// <summary>
        /// Définit des éléments dans l'ensemble de recherche.
        /// </summary>
        /// <param name="elements">Liste des cibles</param>
        public Set(string[] elements) : this(elements, Mode.Whitelist) { }

        /// <summary>
        /// Définit l'ensemble de recherche selon un mode de filtrage spécifique.
        /// </summary>
        /// <param name="m">Mode de ciblage</param>
        public Set(Mode m) : this(new string[] { }, m) { }

        /// <summary>
        /// Définit un ensemble de recherche selon un mode et spécifie des éléments à rechercher ou exclure selon le mode choisi.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="m"></param>
        public Set(string[] elements, Mode m)
        {
            this.elements = new HashSet<string>();
            mode = m;
            foreach(string element in elements) {
                add(element);
            }
        }

        /// <summary>
        /// Add an element to the set if not already.
        /// </summary>
        /// <param name="element">Element to add</param>
        public void add(string element)
        {
            if(!elements.Contains(element)) {
                elements.Add(element);
            }
        }

        /// <summary>
        /// Deletes an element in the set if exists.
        /// </summary>
        /// <param name="element">Element to delete</param>
        public void remove(string element)
        {
            if(elements.Contains(element)) {
                elements.Remove(element);
            }
        }

        /// <summary>
        /// Check whetever the element is in the set.
        /// </summary>
        /// <param name="element">element to check</param>
        /// <returns>True if element is in the set</returns>
        public bool contains(string element)
        {
            // O(1) check if using the ALL set. => Every element is in.
            if (this == ALL)
                return true;

            if (this == NONE)
                return false;

            bool isContained = elements.Contains(element);

            switch (mode)
            {
                case Mode.Whitelist:
                    return isContained;

                case Mode.Blacklist:
                    return !isContained;

                // Should never happens
                default:
                    return false;
            }
        }

        override public String ToString()
        {
            String result = "{ ";
            if (this == ALL)
            {
                result += "ALL";
            }
            else if (this == NONE)
            {
                result += "NONE";
            }
            else
            {
                result += mode + ": ";

                foreach (String s in elements)
                {
                    result += s + " ";
                }
            }
            result += "}";
            return result;
        }
    }
}
