/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>
using System;
using GeneticLibrary.Interfaces;
using UnityEngine;

namespace Wrappers
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
			if(v == null) {
				throw new ArgumentNullException();
			}
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
