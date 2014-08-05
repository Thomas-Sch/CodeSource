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
    /// <summary>
    /// Interface for the subhandler which control the simulation.
    /// </summary>
    public interface ISimControl
    {
        /// <summary>
        /// Gets or sets the speed of the simulation.
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Initialize the simulation.
        /// </summary>
        /// <param name="parameters">The parameters of the simulation.</param>
        void Init(Parameters parameters);

        /// <summary>
        /// Launches the simulation.
        /// I had to use this name because Start() is reserved to unity.
        /// </summary>
        void Play();

        /// <summary>
        /// Resumes the simulation after a pause and set the speed back to the last change.
        /// </summary>
        void Resume();

        /// <summary>
        /// Pauses the simulation.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops the simulation.
        /// </summary>
        void Stop();

        /// <summary>
        /// Returns if the simulation is running or not.
        /// </summary>
        /// <returns>True if it's running, false otherwise.</returns>
        bool IsRunning();

        /// <summary>
        /// Sets the simulation to maximum speed.
        /// </summary>
        void MaximumSpeed();

        /// <summary>
        /// Gets the time elapsed since the start of the simulation.
        /// </summary>
        /// <returns>The elapsed time in miliseconds.</returns>
        long TimeElapsed();
    }
}
