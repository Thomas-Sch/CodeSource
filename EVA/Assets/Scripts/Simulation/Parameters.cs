using System;
using Newtonsoft.Json;

namespace Simulation
{
    /// <summary>
    /// Paramters of the simulation.
    /// </summary>
    public class Parameters
    {
        public string OrganismTag = "Organism";

        /// <summary>
        /// Length of a block for the simulation to compute the average (number of steps).
        /// </summary>
        public int BlockLength { get; set; }

        /// <summary>
        /// Length of the sliding window (number of steps).
        /// </summary>
        public int SlidingWindowLength { get; set; }

        /// <summary>
        /// Initial population of the simulation.
        /// </summary>
        public int InitialPopulation { get; set; }

        /// <summary>
        /// Size of the world.
        /// </summary>
        public int WorldSize { get; set; }

        public int PopulationLimit { get; set; }

        // Organisms settings
        /// <summary>
        /// Percentage of time the organism is a baby in his life.
        /// </summary>
        public float BabyDuration { get; set; } // [%]

        /// <summary>
        /// Percentage of time the organism is a teen in his life.
        /// </summary>
        public float TeenDuration { get; set; } // [%]

        /// <summary>
        /// Length of the organism sight in meters.
        /// It's used to find new parteners for reproduction.
        /// </summary>
        public float OrganismSight { get; set; } // [Length in meters]

        /// <summary>
        /// Duration of the time. The parents of a organism cannot have a new child.
        /// </summary>
        public int NoNewChildDuration { get; set; } // [Number of updates]
    }
}