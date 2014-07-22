using System;
using Newtonsoft.Json;

namespace Simulation
{
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

        /// <summary>
        /// Duration of the time. The parents of a organism cannot have a new child.
        /// </summary>
        public int NoNewChildDuration { get; set; } // [Number of updates]
    }
}