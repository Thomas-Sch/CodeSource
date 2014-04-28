using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.Interfaces;
using UnityEngine;

namespace GeneticLibrary.Wrappers
{
    class WVector3 : IDeepClonable
    {
		public Vector3 Value {get; set;}

		public WVector3(float x, float y, float z) {
			Value = new Vector3(x,y,z);
		}

		public float x {get { return Value.x;}}
		public float y {get { return Value.y;}}
		public float z {get { return Value.z;}}

		public WVector3(Vector3 v) {
			Value = v;
		}
        public WVector3() : this(0,0,0) { }
        public object DeepClone()
        {
			return new WVector3(Value.x, Value.y, Value.z);
        }

		public override String ToString() {
			return "(" + Math.Round(Value.x, 3) + "," + Math.Round(Value.y, 3) + "," + Math.Round(Value.z, 3) + ")";
		}
    }
}
