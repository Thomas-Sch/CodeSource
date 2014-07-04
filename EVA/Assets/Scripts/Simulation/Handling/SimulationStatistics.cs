/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 

using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Simulation.Handling
{
    class SimulationStatistics : ISimStats
    {
        private static string file = "Statistics.csv";

        public int interval {get; set;}

        static SimulationStatistics() {
            Logger.getInstance().Save("Step;Time;Distance_Average;Age_Average;Nb_Organism_Alive;Nb_Organism_Dead", file);
        }

        public SimulationStatistics(int interval)
        {
            this.interval = interval;
        }

        public void Log()
        {
            // Log the statistics.
            Logger.getInstance().Save(SimHandler.Instance().Step + ";" + 
                                      SimHandler.Control().TimeElapsed() + ";" + 
                                      AverageDistance() + ";" + 
                                      AverageAge() + ";" + 
                                      NbOrganismAlive() + ";" +
                                      NbOrganismDead(),file);
        }

        public void SaveParameters(Parameters parameter, string file)
        {
            Logger logger = Logger.getInstance();
            logger.File = file;
            logger.Save(JsonConvert.SerializeObject(parameter));
        }

        public float AverageDistance()
        {
            float result = 0;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if(o.IsDead)
                    result += o.Distance;
            }
            result = result / NbOrganismDead();

            return result;
        }

        public float AverageAge()
        {
            float result = 0;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if(o.IsDead)
                    result += o.Age;
            }
            result = result / NbOrganismDead();

            return result;
        }

        public int NbOrganismAlive()
        {
            return SimHandler.PopulationHandler().LivingOrganisms;
        }

        public int NbOrganismDead()
        {
            return SimHandler.PopulationHandler().DeadOrganisms;
        }
    }
}
