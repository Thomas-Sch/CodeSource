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
            InitialPopulation = 50;

            // Organisms
            BirthDuration = 100; // [Number of updates]
            PreAdultDuration = 0.2f; // [%]
            OrganismSight = 10F; // [Length]

            // Movement settings
            MovementTurnRate = 0.5f; // [0.0 - 1.0]

            // Reproduction settings
            ApproachRate = 0.5f; // [0.0 - 1.0]
            NoNewChildDuration = 100; // [Number of updates]
        }
    }
}
