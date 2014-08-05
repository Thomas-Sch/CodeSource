/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Handling
{
    /// <summary>
    /// Interface for the population handler of the simulation.
    /// </summary>
    public interface IPopulationHandler
    {
        /// <summary>
        /// Gets the list of all the organisms in the simulation.
        /// </summary>
        List<Organism> Organisms { get; }

        /// <summary>
        /// Gets the number of living organisms.
        /// </summary>
        int LivingOrganisms { get; }

        /// <summary>
        /// Gets the number of dead organisms.
        /// </summary>
        int DeadOrganisms { get; }

        /// <summary>
        /// Add an organism to the simulation.
        /// </summary>
        /// <param name="prefab">Model of the organism.</param>
        /// <param name="position">Position of the organism.</param>
        /// <param name="rotation">Rotation of the organism.</param>
        /// <param name="percentageBaseSpawn">True if the spawn is controlled by the current amount of population.</param>
        /// <returns>The GameObject of the organism created.</returns>
        GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, bool percentageBaseSpawn);

        /// <summary>
        /// Add an organism to the simulation with a random y-axis rotation.
        /// </summary>
        /// <param name="prefab">Model of the organism.</param>
        /// <param name="position">Position of the organism.</param>
        /// <returns>The GameObject of the organism created.</returns>
        GameObject SpawnWithRandomRotation(GameObject prefab, Vector3 position);

        /// <summary>
        /// Add an organism to the simulation with a random y-axis rotation.
        /// </summary>
        /// <param name="prefab">Model of the organism.</param>
        /// <param name="position">Position of the organism.</param>
        /// <param name="percentageBaseSpawn">True if the spawn is controlled by the current amount of population.</param>
        /// <returns>The GameObject of the organism created.</returns>
        GameObject SpawnWithRandomRotation(GameObject prefab, Vector3 position, bool percentageBaseSpawn);

        /// <summary>
        /// Deletes an organism from the simulation.
        /// </summary>
        /// <param name="o"></param>
        void Kill(Organism o);
    }
}
