/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using UnityEditor;
using System;
using Simulation.Handling;

namespace Simulation.Handling
{

    /// <summary>
    /// Class d'initialisation pour la simulation.
    /// </summary>
    public class SimHandler : MonoBehaviour, ISimHandler
    {
        private static SimHandler instance;

        private static string Path = "Prefabs/";
        public static GameObject A;
        public static GameObject B;
        public static GameObject D;

        // Variables related to the geographic of the terrain.
        public GameObject Terrain; // Unity variable.
        private float semiSideLength;
        private float randomPositionHeight = 2f;

        // Sub-handlers.
        private Control control;
        private SimulationStatistics statistics;
        private PopulationHandler population;

        public Parameters Parameters { get; set; }
        public long Step { get; private set; }

        /// <summary>
        /// Initiate the simulation and instanciate the sub-handler of it.
        /// </summary>
        private SimHandler()
        {
            // The declaration of the singleton is done here because Unity forbides us to use the new keyword on MonoBehaviour objects.
            instance = this;
            control = new Control();
            statistics = new SimulationStatistics(10);
            population = new PopulationHandler(this);

            Step = 0;
        }

        void Awake()
        {
            A = Resources.Load<GameObject>(Path + "Template A");
            B = Resources.Load<GameObject>(Path + "Template B");
            D = Resources.Load<GameObject>(Path + "Template D");

            // This line has to be done in this Unity's method.
            control.Speed = 1.0f;

            if (Terrain == null)
            {
                UnityEngine.Debug.LogError("Terrain is not set !");
            }
            semiSideLength = transform.localScale.x * Terrain.transform.localScale.x * 4;
        }

        void FixedUpdate()
        {
            if (control.IsRunning())
            {
                Step++;

                if (Step % statistics.interval == 0)
                {
                    statistics.Log();
                }
            }
        }

        /// <summary>
        /// Creates an organism on the map.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation) as GameObject;
        }

        public Vector3 GetRandomPosition()
        {
            Vector3 position = new Vector3();
            position.y = randomPositionHeight;
            position.x = UnityEngine.Random.Range(-semiSideLength, semiSideLength);
            position.z = UnityEngine.Random.Range(-semiSideLength, semiSideLength);
            return position;
        }

        /// <summary>
        /// Returns the sub-handler instance who's in charge of the statistics.
        /// </summary>
        /// <returns>The singleton</returns>
        public static ISimStats Statistics()
        {
            if (instance == null)
                throw new Exception("The singleton was not found !");
            return instance.statistics;
        }

        /// <summary>
        /// Returns the sub-handler instance who's in charge of the control of the simulation.
        /// </summary>
        /// <returns>The singleton</returns>
        public static ISimControl Control()
        {
            if (instance == null)
                throw new Exception("The singleton was not found !");
            return instance.control;
        }

        /// <summary>
        /// Returns the sub-handler instance who's in charge of the population of the simulation.
        /// </summary>
        /// <returns>The singleton</returns>
        public static IPopulationHandler PopulationHandler()
        {
            if (instance == null)
                throw new Exception("The singleton was not found !");
            return instance.population;
        }

        /// <summary>
        /// Returns this instance.
        /// </summary>
        /// <returns>The singleton</returns>
        public static ISimHandler Instance()
        {
            if (instance == null)
                throw new Exception("The singleton was not found !");
            return instance;
        }
    }
}
