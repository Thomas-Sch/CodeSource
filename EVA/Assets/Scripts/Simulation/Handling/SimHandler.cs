/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using System;
using Simulation.Handling;

namespace Simulation.Handling
{
    /// <summary>
    /// Handling class for the simulation.
    /// </summary>
    public class SimHandler : MonoBehaviour, ISimHandler
    {
        private static SimHandler instance;

        private static string Path = "Prefabs/";
        public static GameObject A;
        public static GameObject B;
        public static GameObject C;
        public static GameObject D;

        // Variables related to the geographic of the terrain.
        public GameObject Terrain; // Unity variable.
        public GameObject Limits; // Unity variable.

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

            // Temporaire !
            //Parameters = new DefaultParameters();
            Step = 0;
        }

        /// <summary>
        /// Update the size of the terrain
        /// </summary>
        /// <param name="size">The new size</param>
        public void UpdateTerrain(int size)
        {
            Terrain.transform.localScale = new Vector3(size, 1, size);
            semiSideLength = Terrain.transform.localScale.x * Limits.transform.localScale.x / 2;
        }

        void Awake()
        {
            // The others prefabs are desactivated.
            //A = Resources.Load<GameObject>(Path + "Template A");
            //B = Resources.Load<GameObject>(Path + "Template B");
            //C = Resources.Load<GameObject>(Path + "Template C");
            D = Resources.Load<GameObject>(Path + "Template D");

            // This line has to be done in this Unity's method.
            control.Speed = 1.0f;
        }

        /// <summary>
        /// Unity's method for updating physics.
        /// </summary>
        void FixedUpdate()
        {
            if (control.IsRunning())
            {
                Step++;

                if (Step % statistics.interval == 0)
                {
                    statistics.Log();
                }

                if (Step > 250000)
                    control.Stop();
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

        /// <summary>
        /// Get an random position on the map.
        /// </summary>
        /// <returns>The position</returns>
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
