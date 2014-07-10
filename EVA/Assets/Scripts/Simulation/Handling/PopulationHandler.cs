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
    class PopulationHandler : IPopulationHandler
    {
        private SimHandler handler;

        public List<Organism> Organisms {get; private set;}

        public int LivingOrganisms { get; private set; }

        public int DeadOrganisms
        {
            get
            {
                return Organisms.Count - LivingOrganisms;
            }
        }

        public PopulationHandler(SimHandler handler)
        {
            this.handler = handler;
            Organisms = new List<Organism>();
            LivingOrganisms = 0;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = handler.Spawn(prefab, position, rotation);
            Organism organism = gameObject.GetComponent<Organism>();
            if (organism == null)
            {
                throw new Exception("No script is attached to the gameobject!");
            }
            else
            {
                Organisms.Add(organism);
                LivingOrganisms++;
            }
            return gameObject;
        }

        public GameObject SpawnWithRandomRotation(GameObject prefab, Vector3 position)
        {
            Quaternion rotation = UnityEngine.Random.rotation;
            Vector3 r = rotation.eulerAngles;
            r.x = 0;
            r.z = 0;
            rotation.eulerAngles = r;

            return Spawn(prefab, position, rotation);
        }

        public void Kill(Organism o)
        {
            o.IsDead = true;
            LivingOrganisms--;
            o.gameObject.SetActive(false);

            if (LivingOrganisms <= 0)
            {
                // Last log of the simulation.
                SimHandler.Statistics().Log();
                //SimHandler.Control().Stop();
            }
        }
    }
}
