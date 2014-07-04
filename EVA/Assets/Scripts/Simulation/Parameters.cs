using System;
using Newtonsoft.Json;

namespace Simulation
{
    public class Parameters
    {
        public string OrganismTag = "Organism";

        /// <summary>
        /// Initial population of the simulation.
        /// </summary>
        public int InitialPopulation { get; set; }

        // Organisms settings
        /// <summary>
        /// Duration of the birth for one organism.
        /// </summary>
        public int BirthDuration { get; set; } // [Number of updates]

        /// <summary>
        /// Percentage of time the organism is a preadult in his life.
        /// </summary>
        public float PreAdultDuration { get; set; } // [%]

        /// <summary>
        /// Length of the organism sight in meters.
        /// It's used to find new parteners for reproduction.
        /// </summary>
        public float OrganismSight { get; set; } // [Length in meters]

        // Movement settings
        /// <summary>
        /// Turn rate of the organisms when they move.
        /// </summary>
        public float MovementTurnRate { get; set; } // [0.0 - 1.0]

        // Reproduction settings
        /// <summary>
        /// Approach rate of the organism when they founded a partner.
        /// </summary>
        public float ApproachRate { get; set; } // [0.0 - 1.0]

        /// <summary>
        /// Duration of the time. The parents of a organism cannot have a new child.
        /// </summary>
        public int NoNewChildDuration { get; set; } // [Number of updates]
    }
}