//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace GeneticCode
{
	public class Vector3 {

        public Vector3() : this(0, 0, 0) {}

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public double x { get; set; }

        public double y { get; set; }

        public double z { get; set; }

        override public String ToString() {
            return "(" + Math.Round(x, 3) + "," + Math.Round(y, 3) + "," + Math.Round(z, 3) + ")";
        }
	}
}

