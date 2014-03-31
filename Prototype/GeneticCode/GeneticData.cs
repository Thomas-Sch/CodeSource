using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.Interfaces;
using GeneticCode.Mutations;

namespace GeneticCode
{
    class GeneticData : IEnumerable, ICloneable
    {
        // Tag of the geneticData.
        public String tag { get; private set; }

        private Dictionary<String, Vector3> data = new Dictionary<String, Vector3>();

        public GeneticData() : this("") {}

        public GeneticData(String s)
        {
            tag = s;
        }

        public void set(String element, Vector3 data)
        {
            this.data[element] = data;
        }

        public Vector3 get(String element) {
            return this.data[element];
        }

        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        override public string ToString()
        {
            string result = "";
            foreach (KeyValuePair<String, Vector3> entry in data)
            {
                result += entry.ToString() + " ";
            }
            return result;
        }

        public object Clone()
        {
            var clone = new GeneticData(tag);

            foreach (KeyValuePair<String, Vector3> entry in data)
            {
                clone.set(new String(entry.Key.ToCharArray()), new Vector3(entry.Value));
            }
            return clone;
        }
    }
}
