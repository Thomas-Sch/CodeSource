/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 

using System;
using UnityEngine;

namespace Simulation.Handling
{
    public interface ISimHandler
    {
        /// <summary>
        /// The parameters of the simulation.
        /// </summary>
        Parameters Parameters { get; set; }

        /// <summary>
        /// Update the size of the terrain depending on the paramters of the simulatoin.
        /// </summary>
        void UpdateTerrain(int size);

        /// <summary>
        ///  The step of the simulation.
        /// </summary>
        long Step { get;}

        /// <summary>
        /// Gives a random position on the terrain.
        /// </summary>
        /// <returns>The position</returns>
        Vector3 GetRandomPosition();
    }
}
