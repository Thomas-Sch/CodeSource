/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 

using System;

namespace Simulation.Handling
{
    /// <summary>
    /// Interface for the subhandler of the statistics.
    /// </summary>
    public interface ISimStats
    {
        /// <summary>
        /// Gets or sets the interval between two statistics report.
        /// </summary>
        int interval { get; set; }

        /// <summary>
        /// Returns the cumulative average distance traveled by the organisms.
        /// </summary>
        /// <returns>The distance in meters.</returns>
        float AverageDistanceCumulative();

        /// <summary>
        /// Returns the average distance by block traveled by the organisms.
        /// </summary>
        /// <returns>The distance in meters.</returns>
        float AverageDistanceBlock();

        /// <summary>
        /// Returns the average distance by sliding window traveled by the organisms.
        /// </summary>
        /// <returns>The distance in meters.</returns>
        float AverageDistanceSlidingWindow();

        /// <summary>
        /// Returns the cumulative average age of all the organisms.
        /// </summary>
        /// <returns>The age.</returns>
        float AverageAgeCumulative();

        /// <summary>
        /// Returns the block average age of all the organisms.
        /// </summary>
        /// <returns>The age.</returns>
        float AverageAgeBlock();

        /// <summary>
        /// Returns the sliding window average age of all the organisms.
        /// </summary>
        /// <returns>The age.</returns>
        float AverageAgeSlidingWindow();

        /// <summary>
        /// Returns the number of alive organisms.
        /// </summary>
        /// <returns>The number of alive organisms.</returns>
        int NbOrganismAlive();

        /// <summary>
        /// Returns the number of dead organisms.
        /// </summary>
        /// <returns>The number of dead organisms.</returns>
        int NbOrganismDead();

        /// <summary>
        /// Saves the parameters of the simulation.
        /// </summary>
        /// <param name="parameter">Parameters of the simulation.</param>
        /// <param name="file">The file where the parameters are saved.</param>
        void SaveParameters(Parameters parameter, string file);

        /// <summary>
        /// Logs the statistics for the current step of the simulation.
        /// </summary>
        void Log();
    }
}
