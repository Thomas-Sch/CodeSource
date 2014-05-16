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
			this.probability = normalizeValue(probability);
        }

		/// <summary>
		/// Normalizes the value between min and max.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="value">Value.</param>
		private static double normalizeValue(double value) {
			const double min = 0.0;
			const double max = 1.0;

			if(value > max) {
				value = max;
			} else if (value < min) {
				value = min;
			}
			return value;
		}

		/// <summary>
		/// Probability check.
		/// </summary>
        public Boolean Test()
        {
			return Probability.Test(normalizeValue(probability));
        }

		/// <summary>
		/// Probability check.
		/// </summary>
		public static Boolean Test(Double probability) {
			return generator.NextDouble() < probability;
		}
        
    }
}
