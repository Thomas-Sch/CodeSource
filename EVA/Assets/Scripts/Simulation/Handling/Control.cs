/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
//using UnityEditor;
using UnityEngine;

namespace Simulation.Handling
{
    /// <summary>
    /// Subhandler of the simulation handler. This class take care of the control of the application.
    /// </summary>
    class Control : ISimControl
    {        
        private Stopwatch time;

        public float lastSpeed;

        public float Speed
        {
            get
            {
                return Time.timeScale;
            }

            set
            {
                Time.timeScale = value;
            }
        }

        public Control()
        {
            time = new Stopwatch();
        }

        /// <summary>
        /// Initiate the simulation.
        /// </summary>
        /// <param name="parameters">Paramters of the simulation</param>
        public void Init(Parameters parameters)
        {
            SimHandler.Instance().Parameters = parameters;
            SimHandler.Statistics().SaveParameters(parameters, "Parameters.json");
        }

        /// <summary>
        /// Start the simulation the first time.
        /// </summary>
        public void Play()
        {
            SimHandler.Instance().UpdateTerrain(SimHandler.Instance().Parameters.WorldSize);
            for (var i = 0; i < SimHandler.Instance().Parameters.InitialPopulation; ++i)
            {
                SimHandler.PopulationHandler().SpawnWithRandomRotation(SimHandler.D, SimHandler.Instance().GetRandomPosition(), false);
            }
            time.Start();
        }

        /// <summary>
        /// Resume the simulation.
        /// </summary>
        public void Resume()
        {
            Speed = lastSpeed;
            time.Start();
        }

        /// <summary>
        /// Pause the simulation.
        /// </summary>
        public void Pause()
        {
            lastSpeed = Speed;
            Speed = 0.0f;
            time.Stop();
        }

        /// <summary>
        /// Stop the simulation and close the window.
        /// </summary>
        public void Stop()
        {
            time.Stop();
            //EditorApplication.isPlaying = false;
            Application.Quit();
        }

        /// <summary>
        /// Returns if the simulation is running.
        /// </summary>
        /// <returns>True if the simulation is running</returns>
        public bool IsRunning()
        {
            return time.IsRunning;
        }

        /// <summary>
        /// Sets the simulation at maximum speed.
        /// </summary>
        public void MaximumSpeed()
        {

            lastSpeed = Speed;
            Speed = 100f;
        }

        /// <summary>
        /// Returns the real time elapsed since the beginning.
        /// </summary>
        /// <returns>The time elapsed</returns>
        public long TimeElapsed()
        {
            return time.ElapsedMilliseconds;
        }
    }
}
