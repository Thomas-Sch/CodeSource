/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

namespace Simulation {

    /// <summary>
    /// Starting and general parameters for the simulation. Parents have 1 child per reproduction.
    /// </summary>
    public class DefaultParameters : Parameters
    {
        public DefaultParameters()
        {
            // Simulation
            InitialPopulation = 100;
            WorldSize = 30;
            PopulationLimit = 150;

            // Statistics
            BlockLength = 5000;
            SlidingWindowLength = 5000;

            // Organisms
            BabyDuration = 0.05f; // [%]
            TeenDuration = 0.2f; // [%]
            OrganismSight = 5f; // [Length]

            // Movement settings

            // Reproduction settings
            NoNewChildDuration = 150; // [Number of updates]
        }
    }
}
