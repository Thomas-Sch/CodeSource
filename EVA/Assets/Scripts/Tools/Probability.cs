/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>
/// 
using System;

namespace Tools
{
	/// <summary>
	/// This class is used to reprent a probability.
	/// </summary>
    public class Probability
    {
        private Double probability;
        private static Random generator;

        static Probability()
        {
            generator = new Random();
        }

        /// <summary>
        /// Instanciate a probability based object to pass tests.
        /// </summary>
        /// <param name="probability">Probability given to happens</param>
        public Probability(Double probability)
        {
			if (probability > 1.0)
				this.probability = 1.0;
			else if (probability < 0.0)
				this.probability = 0.0;
			else
				this.probability = probability;
        }

        public Boolean Test()
        {
            return Probability.Test(probability);
        }

		public static Boolean Test(Double probability) {
			return generator.NextDouble() < probability;
		}
        
    }
}
