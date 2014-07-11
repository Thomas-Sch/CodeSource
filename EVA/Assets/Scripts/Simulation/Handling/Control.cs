/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Simulation.Handling
{
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

        public void Init(Parameters parameters)
        {
            SimHandler.Instance().Parameters = parameters;
            SimHandler.Statistics().SaveParameters(parameters, "Parameters.json");
        }

        public void Play()
        {
            for (var i = 0; i < SimHandler.Instance().Parameters.InitialPopulation; ++i)
            {
                SimHandler.PopulationHandler().SpawnWithRandomRotation(SimHandler.D, SimHandler.Instance().GetRandomPosition());
            }
            time.Start();
        }

        public void Resume()
        {
            Speed = lastSpeed;
            time.Start();
        }

        public void Pause()
        {
            lastSpeed = Speed;
            Speed = 0.0f;
            time.Stop();
        }

        public void Stop()
        {
            time.Stop();
            EditorApplication.isPlaying = false;
            Application.Quit();
        }


        public bool IsRunning()
        {
            return time.IsRunning;
        }

        public void MaximumSpeed()
        {

            lastSpeed = Speed;
            Speed = 100f;
        }

        public long TimeElapsed()
        {
            return time.ElapsedMilliseconds;
        }

    }
}
